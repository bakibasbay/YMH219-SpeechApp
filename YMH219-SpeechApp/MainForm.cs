using SpeechApp.Controls;
using SpeechApp.Models;
using SpeechApp.Services;

namespace SpeechApp;

// Ana pencere: Text-to-Speech ve Speech-to-Text bölümlerini barındırır
public partial class MainForm : Form
{
    private const string AppTitle = "YMH219 - Speech App";

    private readonly TextToSpeechService _tts = new();

    // Okumanın kullanıcı tarafından durdurulduğunu ayırt etmek için
    private bool _ttsStopRequested;

    public MainForm()
    {
        InitializeComponent();

        // Servis olaylarına abone olunur
        _tts.StateChanged += Tts_StateChanged;
        _tts.SpeakFinished += Tts_SpeakFinished;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        LoadVoices();
        ShowPage(showTts: true);
        UpdateUiState();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // Ses kaynakları kapatılır
        _tts.Dispose();
        base.OnFormClosed(e);
    }

    #region Sekme geçişi

    // Üst menüdeki sekme butonlarına göre ilgili paneli gösterir
    private void ShowPage(bool showTts)
    {
        pnlTts.Visible = showTts;
        pnlStt.Visible = !showTts;
        btnNavTts.Variant = showTts ? ButtonVariant.Primary : ButtonVariant.Ghost;
        btnNavStt.Variant = showTts ? ButtonVariant.Ghost : ButtonVariant.Primary;
    }

    private void BtnNavTts_Click(object? sender, EventArgs e) => ShowPage(showTts: true);

    private void BtnNavStt_Click(object? sender, EventArgs e) => ShowPage(showTts: false);

    #endregion

    #region Text-to-Speech

    // Kurulu sesleri listeye doldurur; mümkünse sistem diline uygun sesi seçer
    private void LoadVoices()
    {
        cmbVoice.Items.Clear();
        var voices = _tts.GetVoices();
        foreach (var voice in voices)
            cmbVoice.Items.Add(voice);

        if (voices.Count == 0)
        {
            lblVoiceHint.Text = "Bu bilgisayarda kurulu bir TTS sesi bulunamadı.";
            lblVoiceHint.ForeColor = Color.FromArgb(185, 28, 28);
            return;
        }

        string uiCulture = Thread.CurrentThread.CurrentUICulture.Name;
        var preferred = voices.FirstOrDefault(v => v.Culture == uiCulture) ?? voices[0];
        cmbVoice.SelectedItem = preferred;

        if (!voices.Any(v => v.Culture.StartsWith("tr", StringComparison.OrdinalIgnoreCase)))
            lblVoiceHint.Text = "Kurulu Türkçe ses yok; Türkçe metinler bu sesin aksanıyla okunur.";
    }

    private void CmbVoice_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbVoice.SelectedItem is not VoiceOption voice)
            return;

        try
        {
            _tts.SelectVoice(voice.Name);
        }
        catch (ServiceException ex)
        {
            ShowError(ex);
        }
    }

    private void BtnSpeak_Click(object? sender, EventArgs e)
    {
        try
        {
            _tts.Rate = tbRate.Value;
            _ttsStopRequested = false;
            _tts.Volume = tbVolume.Value;
            _tts.Speak(rtbTtsText.Text);
            // Okuma sırasında metin değişirse vurgulama kayar; bu yüzden kilitlenir
            rtbTtsText.ReadOnly = true;
            SetStatusMessage("Metin okunuyor…");
        }
        catch (ServiceException ex)
        {
            ShowError(ex);
        }

        UpdateUiState();
    }

    private void BtnPauseResume_Click(object? sender, EventArgs e)
    {
        if (_tts.IsPaused)
        {
            _tts.Resume();
            SetStatusMessage("Okumaya devam ediliyor…");
        }
        else
        {
            _tts.Pause();
            SetStatusMessage("Okuma duraklatıldı.");
        }

        UpdateUiState();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
        _ttsStopRequested = true;
        _tts.Stop();
        SetStatusMessage("Okuma durduruldu.");
        UpdateUiState();
    }

    private void BtnTtsClear_Click(object? sender, EventArgs e)
    {
        rtbTtsText.Clear();
        rtbTtsText.Focus();
        SetStatusMessage("Metin temizlendi.");
    }

    private void RtbTtsText_TextChanged(object? sender, EventArgs e) => UpdateUiState();

    private void TbRate_ValueChanged(object? sender, EventArgs e)
    {
        int rate = tbRate.Value;
        lblRateValue.Text = rate switch
        {
            0 => "Normal (0)",
            < 0 => $"Yavaş ({rate})",
            _ => $"Hızlı (+{rate})"
        };
        _tts.Rate = rate;
    }

    private void TbVolume_ValueChanged(object? sender, EventArgs e)
    {
        lblVolumeValue.Text = $"%{tbVolume.Value}";
        _tts.Volume = tbVolume.Value;
    }

    private void Tts_StateChanged(object? sender, EventArgs e) => RunOnUi(UpdateUiState);

    private void Tts_SpeakFinished(object? sender, Exception? error)
    {
        RunOnUi(() =>
        {
            rtbTtsText.ReadOnly = false;
            if (error is not null)
            {
                ShowError(new ServiceException("Ses Aygıtı Hatası",
                    "Okuma sırasında ses çıkış aygıtında bir hata oluştu.\n\nAyrıntı: " + error.Message, error));
                SetStatusMessage("Okuma hatayla sonlandı.");
            }
            else if (!_ttsStopRequested)
            {
                SetStatusMessage("Okuma tamamlandı.");
            }

            UpdateUiState();
        });
    }

    #endregion

    #region Ortak yardımcılar

    // Butonların aktif/pasif durumunu ve durum göstergesini günceller
    private void UpdateUiState()
    {
        bool ttsBusy = _tts.IsBusy;
        bool hasText = !string.IsNullOrWhiteSpace(rtbTtsText.Text);

        btnSpeak.Enabled = hasText && !ttsBusy && cmbVoice.Items.Count > 0;
        btnPauseResume.Enabled = ttsBusy;
        btnPauseResume.Text = _tts.IsPaused ? "▶  Devam Et" : "❚❚  Duraklat";
        btnStop.Enabled = ttsBusy;
        btnTtsClear.Enabled = hasText && !ttsBusy;
        cmbVoice.Enabled = !ttsBusy && cmbVoice.Items.Count > 0;

        statusIndicator.Status = _tts.IsPaused ? AppStatus.Duraklatildi
            : _tts.IsSpeaking ? AppStatus.Okunuyor
            : AppStatus.Hazir;

        Text = statusIndicator.Status == AppStatus.Hazir
            ? AppTitle
            : $"{AppTitle} — {StatusIndicator.Describe(statusIndicator.Status).Text}";
    }

    private void SetStatusMessage(string message) => lblStatusMessage.Text = message;

    private void ShowError(ServiceException ex) =>
        MessageBox.Show(this, ex.Message, ex.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    // Servis olayları farklı bir iş parçacığından gelirse arayüz iş parçacığına aktarılır
    private void RunOnUi(Action action)
    {
        if (IsDisposed || Disposing)
            return;

        if (InvokeRequired)
            BeginInvoke(action);
        else
            action();
    }

    #endregion
}

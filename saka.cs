using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

class Program {
    // Windows Ses Kontrolü için API
    [DllImport("user32.dll")]
    static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    
    private const int WM_APPCOMMAND = 0x319;
    private const int APPCOMMAND_VOLUME_UP = 0xA0000;
    private const int APPCOMMAND_VOLUME_DOWN = 0x90000;

    static void Main() {
        // 1. SESİ %100 YAP
        IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
        
        // Önce sesi tamamen sıfırla
        for (int i = 0; i < 50; i++) 
            SendMessageW(handle, WM_APPCOMMAND, handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
            
        // Sesi maksimuma çıkar (%100)
        for (int i = 0; i < 50; i++) 
            SendMessageW(handle, WM_APPCOMMAND, handle, (IntPtr)APPCOMMAND_VOLUME_UP);

        // 2. MSEDGE.EXE BAŞLAT VE URL GİR
        Process.Start("msedge.exe");
        Thread.Sleep(3000); 

        SendKeys.SendWait("^l"); 
        Thread.Sleep(500);
        
        string url = "https://www.youtube.com/watch?v=ywthKNqI7uI";
        foreach (char c in url) {
            SendKeys.SendWait(c.ToString());
            Thread.Sleep(40); 
        }
        SendKeys.SendWait("{ENTER}");
        
        // Sayfanın ve video oynatıcısının yüklenmesi için bekle
        Thread.Sleep(6000); 

        // 3. VİDEOYU BAŞLATMAK İÇİN SPACE'E BAS
        // Önce sayfaya odaklandığından emin olmak için bir kez tıklama simülasyonu veya direkt Space
        SendKeys.SendWait(" "); 
        Thread.Sleep(1000);

        // 4. NOTEPAD AÇ VE MESAJ YAZ
        SendKeys.SendWait("^{ESC}"); 
        Thread.Sleep(800);
        SendKeys.SendWait("notepad");
        Thread.Sleep(500);
        SendKeys.SendWait("{ENTER}");
        Thread.Sleep(1500);
        
        SendKeys.SendWait("SESI ACTIM VE VIDEONU BASLATTIM :D");
    }
}
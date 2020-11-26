using System.Windows;

namespace PasswordManager.Services
{
    public class CopyService : ICopyService
    {
        public void CopyText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                Clipboard.SetText(text);
        }
    }
}

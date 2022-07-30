
using System;

namespace TestNinja.Fundamentals
{
    public class ErrorLogger
    {
        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged; 
        
        public void Log(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            LastError = error;

            // Write the log to a storage
            // ...

            //ErrorLogged?.Invoke(this, Guid.NewGuid());
            OnErrorLogged(Guid.NewGuid());
        }

        /*
            private、protect 方法 不該使用單元測試，因為他們是實作細節，常常修改，只要修改(如:有參數改成無參數)你的測試就爆了
            如果你的public方法裡面call a private method ,and that private call another private method
            然後private method 裡面都有5~6行邏輯，那你就該改寫了，可能這些private 方法可以獨立出一個class ，然後寫成Public 方便測試
        */
        protected virtual void OnErrorLogged(Guid errorid)
        {
            ErrorLogged?.Invoke(this, errorid);
        }
    }
}
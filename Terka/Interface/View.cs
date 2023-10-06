using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terka.Interface
{
    public class Viewa
    {
        public event EventHandler DataRequested;

        public void ShowData(string data)
        {
            Form1 form = new Form1();
            form.UsernameTextBox.Text = data;
        }

        private void LoginButton_Click_1(object sender, EventArgs e)
        {
            DataRequested?.Invoke(this, EventArgs.Empty);
        }

        public static implicit operator Viewa(Form3 v)
        {
            throw new NotImplementedException();
        }
    }
}

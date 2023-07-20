using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terka.Models;

namespace Terka.Controllers
{
    internal class FormController
    {
        private Item model;
        private Form3 view;

        public FormController() { }
        public FormController(Item model, Form3 view)
        {
            this.model = model;
            this.view = view;
        }

        public void ShowView()
        {
            view.ShowDialog();
        }
    }
}

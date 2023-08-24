using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terka.Interface;
using Terka.Models;

namespace Terka.Controllers
{
    internal class FormController
    {
        private ItemRepository? _view = null;
        private Item model;
        private Form3 view;

        public FormController() { }
        public FormController(Item model, Form3 view, ItemRepository itemRepository)
        {
            Contract.Requires(itemRepository != null, "View can’t be null");
            this.model = model;
            this.view = view;
        }

        public void ShowView()
        {
            view.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
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
        private Viewa view;

        public FormController(Item model, Viewa view) {
            this.model = model;
            this.view = view;

            view.DataRequested += (sender, args) => GetData();
        }
        private void GetData()
        {
            string data = model.GetData();
            view.ShowData(data);
        }
        public FormController(Item model, Form3 view, ItemRepository itemRepository)
        {
            Contract.Requires(itemRepository != null, "View can’t be null");
            this.model = model;
            this.View = view;
        }

        public Interface.Viewa View { get => view; set => view = value; }

    }
}

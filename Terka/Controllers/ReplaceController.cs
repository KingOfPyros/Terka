using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    internal class ReplaceController
    {
        private IReplaceView _view = null;
        private Func<List<Item>> ReplaceSymbolsGet;

        public ReplaceController(IReplaceView view)
        {
            Contract.Requires(view != null, "View can’t be null");

            _view = view;
        }
        public void LoadEmptyReplaces()
        {
            var emptyReplaces = new List<Item>();

            {
                ReplaceSymbolsGet =
                 () =>
                 {
                     return emptyReplaces;
                 };
            };

            Item focusedReplace = null;
            var loadedReplaces = new List<Item>();
            var selectedReplaces = new List<Item>();
            var enableCreate = false;
            var enableDelete = false;

            Assert.IsNotNull(loadedReplaces, "После создания контроллераи и его открытия список замен не должен быть null");
            Assert.IsTrue(loadedReplaces.Count == 0, "После создания контроллера и его открытия список замен не должен быть пустым, так как в него загружен пустой список");
            Assert.IsNull(focusedReplace, "После создания контроллера и его открытия активной замены быть не должно");
            Assert.IsNotNull(selectedReplaces, "После создания контроллера список замен не может быть null");
            Assert.IsTrue(selectedReplaces.Count == 0, "После создания контроллера и его открытия в список выбраных замен должен быть пустой");
            Assert.IsTrue(enableCreate, "После создания контроллера должна быть доступна возможность создавать замены");
            Assert.IsFalse(enableDelete, "После создания контроллера не должно быть доступно удаление, так как список замен пустой");
        }
    }
}

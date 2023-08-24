using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terka.Models;

namespace Terka.Interface
{
    internal interface IReplaceView
    {
        bool EnableCreate
        {
            get;
            set;
        }

        bool EnableDelete
        {
            get;
            set;
        }

        List<Item> Replaces
        {
            set;
        }
        Item FocusedReplace
        {
            get;
            set;
        }

        List<Item> SelectedReplaces
        {
            get;
            set;
        }

        event EventHandler Closing;

        event EventHandler Creating;

        event EventHandler Deleting;

        event EventHandler FocusedChanged;

        void Open();

        void Close();

        void RefreshView();
    }
}

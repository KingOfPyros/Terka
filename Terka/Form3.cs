using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terka.Interface;
using Terka.Models;

namespace Terka
{
    public partial class Form3 : Form
    {
        private List<Item> _items;
        private FlowLayoutPanel _flowLayoutPanel;

        public Form3()
        {
            InitializeComponent();
            InitializeItems();
            InitializeFlowLayoutPanel();
            LoadItems();
        }

        private void InitializeItems()
        {
            ItemRepository itemRepository = new ItemRepository();
            _items = itemRepository.GetAllItems();
        }

        private void InitializeFlowLayoutPanel()
        {
            _flowLayoutPanel = new FlowLayoutPanel();
            _flowLayoutPanel.Dock = DockStyle.Fill;
            Controls.Add(_flowLayoutPanel);
        }

        private void LoadItems()
        {
            foreach (var item in _items)
            {
                Panel itemPanel = new Panel();
                itemPanel.Size = new Size(200, 800);
                itemPanel.BorderStyle = BorderStyle.FixedSingle;

                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Dock = DockStyle.Fill;
                using (MemoryStream ms = new MemoryStream(item.ImagePath))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
                itemPanel.Controls.Add(pictureBox);
                Label nameLabel = new Label();
                nameLabel.Text = item.Name;
                nameLabel.Font = new Font(nameLabel.Font, FontStyle.Bold);
                nameLabel.Dock = DockStyle.Bottom;
                itemPanel.Controls.Add(nameLabel);

                Label descriptionLabel = new Label();
                descriptionLabel.Text = item.Description;
                descriptionLabel.Dock = DockStyle.Bottom;
                descriptionLabel.AutoSize = false;
                descriptionLabel.Width = 200;
                descriptionLabel.Height = 600;
                descriptionLabel.TextAlign = ContentAlignment.TopLeft;
                itemPanel.Controls.Add(descriptionLabel);


                _flowLayoutPanel.Controls.Add(itemPanel);
            }
        }
    }
}

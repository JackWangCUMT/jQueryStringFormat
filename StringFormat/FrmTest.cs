using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using jQueryString;
namespace StringFormat
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            string Name = "jack";
            int ID = 200;
            string template = "exec func($Name,$ID)";
            string parseText = template.jQueryStringFormat(template, new { ID, Name });
           


             template = "the $Name who ID is $$ID";
             parseText = template.jQueryStringFormat(template, new Person { ID = "2", Name = "JackWang" });

            this.Text = parseText;

        }
        public class Person
        {
            public string ID { get; set; }
            public string Name { get; set; }

        }
    }
}

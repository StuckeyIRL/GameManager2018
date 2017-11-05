using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace GameManager2018
{
    public partial class Form1 : Form
    {
        string SQLHeader = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Games.accdb";
        int page = 0;
        int maxpage = 0;
        int totalPlays = 0;
        string sSQL = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadPage(object sender, EventArgs e)
        {
            quickYear.Text = DateTime.Now.Year.ToString();
            quickNewFavorites.Font = new Font(quickNewFavorites.Font, FontStyle.Regular);
            quickYear.Font = new Font(quickYear.Font, FontStyle.Regular);
            quickAll.Font = new Font(quickAll.Font, FontStyle.Regular);
            quickOldFavorites.Font = new Font(quickOldFavorites.Font, FontStyle.Regular);
            Control control = (Control)sender;
            OleDbConnection conX = new OleDbConnection(SQLHeader);
            DataSet dsX = new DataSet();
            MessageBox.Show("Test");
            switch (control.Name)
            {
                case "quickNewFavorites":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by Played DESC";
                    quickNewFavorites.Font = new Font(quickNewFavorites.Font, FontStyle.Italic);
                    break;
                case "quickSearch":
                    page = 0;
                    textSearch.Text = textSearch.Text.Replace("'", "");
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where DisplayTitle like '%" + textSearch.Text + "%' or Author like '%" + textSearch.Text + "%' or TechnicalTitle like '%" + textSearch.Text + "%' order by Played DESC";
                    break;
                case "quickYear":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Yr = " + DateTime.Now.Year.ToString() + " order by Played DESC";
                    quickYear.Font = new Font(quickYear.Font, FontStyle.Italic);
                    break;
                case "quickAll":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main order by Played DESC";
                    quickAll.Font = new Font(quickAll.Font, FontStyle.Italic);
                    break;
                case "quickOldFavorites":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite like '20%' and Favorite not like '" + DateTime.Now.Year.ToString() + "' order by Played DESC";
                    quickOldFavorites.Font = new Font(quickOldFavorites.Font, FontStyle.Italic);
                    break;
                default:
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by DisplayTitle";
                    break;
            }
        }
    }
}

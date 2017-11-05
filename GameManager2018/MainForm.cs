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
    public partial class MainForm : Form
    {
        private string SQLHeader = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Games.accdb";
        private int page = 0;
        private int maxpage = 0;
        private int totalPlays = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadPage(object sender, EventArgs e)
        {
            currentYear.Text = DateTime.Now.Year.ToString();
            currentYear.Font = new Font(currentYear.Font, FontStyle.Regular);
            currentFavorites.Font = new Font(currentFavorites.Font, FontStyle.Regular);
            allGames.Font = new Font(allGames.Font, FontStyle.Regular);
            oldFavorites.Font = new Font(oldFavorites.Font, FontStyle.Regular);
            Control control = (Control)sender;
            OleDbConnection conX = new OleDbConnection(SQLHeader);
            DataSet dsX = new DataSet();

            string sSQL = "";

            switch (control.Name)
            {
                case "search":
                    page = 0;
                    textSearch.Text = textSearch.Text.Replace("'", "");
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where DisplayTitle like '%" + textSearch.Text + "%' or Author like '%" + textSearch.Text + "%' or TechnicalTitle like '%" + textSearch.Text + "%' order by Played DESC";
                    break;
                case "currentFavorites":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by Played DESC";
                    currentFavorites.Font = new Font(currentFavorites.Font, FontStyle.Italic);
                    break;
                case "currentYear":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Yr = " + DateTime.Now.Year.ToString() + " order by Played DESC";
                    currentYear.Font = new Font(currentYear.Font, FontStyle.Italic);
                    break;
                case "allGames":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main order by Played DESC";
                    allGames.Font = new Font(allGames.Font, FontStyle.Italic);
                    break;
                case "oldFavorites":
                    page = 0;
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite like '20%' and Favorite not like '" + DateTime.Now.Year.ToString() + "' order by Played DESC";
                    oldFavorites.Font = new Font(oldFavorites.Font, FontStyle.Italic);
                    break;
                default: // Default to new favorites
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by DisplayTitle";
                    break;
            }
        }
    }
}

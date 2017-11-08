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
        int increment = 16;
        //int difference = 0;
        string originalSender;
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
            originalSender = control.Name;
            switch (control.Name)
            {
                case "search":
                    textSearch.Text = textSearch.Text.Replace("'", "");
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where DisplayTitle like '%" + textSearch.Text + "%' or Author like '%" + textSearch.Text + "%' or TechnicalTitle like '%" + textSearch.Text + "%' order by DisplayTitle";
                    break;
                case "currentFavorites":
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by DisplayTitle";
                    currentFavorites.Font = new Font(currentFavorites.Font, FontStyle.Italic);
                    break;
                case "currentYear":
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Yr = " + DateTime.Now.Year.ToString() + " order by DisplayTitle";
                    currentYear.Font = new Font(currentYear.Font, FontStyle.Italic);
                    break;
                case "allGames":
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main order by DisplayTitle";
                    allGames.Font = new Font(allGames.Font, FontStyle.Italic);
                    break;
                case "oldFavorites":
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite like '20%' and Favorite not like '" + DateTime.Now.Year.ToString() + "' order by DisplayTitle";
                    oldFavorites.Font = new Font(oldFavorites.Font, FontStyle.Italic);
                    break;
                default: // Default to new favorites
                    sSQL = "select DisplayTitle, TechnicalTitle, [Description], Played from Main where Favorite = " + DateTime.Now.Year.ToString() + " order by DisplayTitle";
                    break;
            }
            string dispTitle = "";
            OleDbDataAdapter daX = new OleDbDataAdapter(sSQL, conX);
            daX.Fill(dsX);
            if (dsX.Tables[0].Rows.Count >= 31)
            {
                for (int i = increment; i < 31; i++)
                {
                    dispTitle = (dsX.Tables[0].Rows[i - increment]["DisplayTitle"]).ToString();
                    Controls["game" + i].Text = (dispTitle);
                }
                int x = 0;
                
                for (int i = increment; i > 0; i--)
                {
                    dispTitle = (dsX.Tables[0].Rows[dsX.Tables[0].Rows.Count - x - 1]["DisplayTitle"]).ToString();
                    Controls["game" + (i - 1)].Text = (dispTitle);
                    x++;
                }
            }
        }

        private void moveUp(object sender, EventArgs e)
        {
            if (increment > 0)
                increment--;
            else
                increment = 31;
            switch (originalSender)
            {
                case "quickSearch":
                    LoadPage(quickSearch, null);
                    break;
                case "currentFavorites":
                    LoadPage(currentFavorites, null);
                    break;
                case "oldFavorites":
                    LoadPage(oldFavorites, null);
                    break;
                case "currentYear":
                    LoadPage(currentYear, null);
                    break;
                default:
                    LoadPage(allGames, null);
                    break;
            }
        }

        private void moveDown(object sender, EventArgs e)
        {
            if (increment < 31)
                increment++;
            else
                increment = 0;
            switch (originalSender)
            {
                case "quickSearch":
                    LoadPage(quickSearch, null);
                    break;
                case "currentFavorites":
                    LoadPage(currentFavorites, null);
                    break;
                case "oldFavorites":
                    LoadPage(oldFavorites, null);
                    break;
                case "currentYear":
                    LoadPage(currentYear, null);
                    break;
                default:
                    LoadPage(allGames, null);
                    break;
            }
        }
    }
}

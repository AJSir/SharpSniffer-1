using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace SharpSniffer
{
   
    public partial class FormFilter : Form
    {
        public FormFilter()
        {
            InitializeComponent();
        }

        private SnifferFilter m_filter;
        
        public void SetFilter(SnifferFilter ft)
        {
            m_filter = ft;
        }

        public SnifferFilter GetFilter()
        {
            return m_filter;
        }

        private void groupBoxIpAddress_Enter(object sender, EventArgs e)
        {

        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            if(m_filter==null)
            {
                m_filter = new SnifferFilter();
            }
            UpdateFromFilter();
  
        }

        bool IsWelformIpString(string text)
        {
            String[] ss = text.Split('.');
            if (ss.Length != 4)
            {
                MessageBox.Show("IP or mask '"+ text + "' should be like 192.128.1.12");
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToInt32(ss[i]) > 255)
                {
                    MessageBox.Show(ss[i] + "is > 255.");
                    return false;
                }
            }
            return true;
        }

        private void buttonIpAdd_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text;
            if (text.Length == 0) return;

            String[] ss = text.Split('/');
            String ip = null;
            String mask = null;
            if (ss.Length == 1)
            {
                ip = text;
                mask = "255.255.255.255";
            }
            else if (ss.Length == 2)
            {
                ip = ss[0];
                mask = ss[1];
            }
            else
            {
                MessageBox.Show("only ip/mask, you have input too many '/'");
                textBox1.Focus();
                return;
            }

            if (!IsWelformIpString(ip))
            {
                textBox1.Focus();
                return;
            }

            if (!IsWelformIpString(mask))
            {
                textBox1.Focus();
                return;
            }
            text = ip + "/" + mask;
            if (listBoxIP.Items.Contains(text))
            {
                MessageBox.Show("Already exists");
                return;
            }
            listBoxIP.Items.Add(ip+"/"+mask);
            textBox1.Text = "";
        }

        private void buttonIpRemove_Click(object sender, EventArgs e)
        {
            if (listBoxIP.SelectedIndex >= 0 && listBoxIP.SelectedIndex < listBoxIP.Items.Count)
            {
                listBoxIP.Items.RemoveAt(listBoxIP.SelectedIndex);
            }
        }

        private void buttonPortAdd_Click(object sender, EventArgs e)
        {
            
            String text = TextBoxPort.Text.Replace(" ","");
            if(text.Length==0) return ;
            if (Convert.ToInt32(text) >= 65535 ||Convert.ToInt32(text)==0)
            {
                MessageBox.Show("Port "+text+" should be between 0 and 65535");
                return;
            }

            if (listBoxPort.Items.Contains(text))
            {
                MessageBox.Show("Port " + text + " already exists");
                return;
            }

            listBoxPort.Items.Add(text);
            TextBoxPort.Text = "";
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxPort.SelectedIndex >= 0 && listBoxPort.SelectedIndex < listBoxPort.Items.Count)
            {
                listBoxPort.Items.RemoveAt(listBoxPort.SelectedIndex);
            }
        }

        void SaveToFile()
        {
            if (m_filter != null)
            {
                UpdateToFilter();
                int lastIndex = Application.ExecutablePath.LastIndexOf('\\');
                String pathFilters = Application.ExecutablePath.Remove(lastIndex, Application.ExecutablePath.Length - lastIndex)
                    + "\\filter\\";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = pathFilters;
                dlg.Filter = "Filter file(*.flt)|*.flt";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    m_filter.Save(dlg.FileName);
                    this.Text = m_filter.Name;
                }
            }
        }


        bool UpdateToFilter()
        {
            if (m_filter!=null)
            {
                m_filter.DataOnly = checkBoxOnlyData.Checked;
                m_filter.PortExluded = checkBoxPortExcluded.Checked;
                m_filter.AddressExcluded = checkBoxIPExcluded.Checked;
                m_filter.Portlist.Clear();
                m_filter.AddressList.Clear();
                for (int i = 0; i < listBoxIP.Items.Count; i++)
                {
                    String[] ss = listBoxIP.Items[i].ToString().Split('/');
                    m_filter.AddressList.Add(new FilterAddress(IPAddress.Parse(ss[0]),
                        IPAddress.Parse(ss[1])));
                }
                for (int i = 0; i < listBoxPort.Items.Count; i++)
                {
                    m_filter.Portlist.Add(Convert.ToUInt16(listBoxPort.Items[i].ToString()));
                }

                m_filter.ProtocolList.Clear();
                if (checkBoxTCP.Checked)
                    m_filter.ProtocolList.Add(ProtocolType.Tcp);
                if (checkBoxUDP.Checked)
                    m_filter.ProtocolList.Add(ProtocolType.Udp);
                return true;
            }

            return false;
        }

        void UpdateFromFilter()
        {
            if (m_filter != null)
            {
                this.Text = "Filter - "+m_filter.Name;
                checkBoxIPExcluded.Checked = m_filter.AddressExcluded;
                checkBoxPortExcluded.Checked = m_filter.PortExluded;
                checkBoxOnlyData.Checked = m_filter.DataOnly;
                if (m_filter.IncludedProtocol(ProtocolType.Tcp))
                    checkBoxTCP.Checked = true;
                if (m_filter.IncludedProtocol(ProtocolType.Udp))
                    checkBoxUDP.Checked = true;

                for (int i = 0; i < m_filter.AddressList.Count; i++)
                {
                    listBoxIP.Items.Add(m_filter.AddressList[i].ip.ToString() + "/"
                        + m_filter.AddressList[i].mask.ToString());
                }

                for (int i = 0; i < m_filter.Portlist.Count; i++)
                {
                    listBoxPort.Items.Add(m_filter.Portlist[i].ToString());
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UpdateToFilter();
            if (m_filter.FilePathName.Length == 0)
            {
                SaveToFile();
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '.' && e.KeyChar != '/'
                && (Keys)e.KeyChar != Keys.Back
                && (Keys)e.KeyChar != Keys.Delete
                && (Keys)e.KeyChar != Keys.Left
                && (Keys)e.KeyChar != Keys.Right
                && Control.ModifierKeys == Keys.None //allow ctrl + V
                )
                e.Handled = true;
        }

        private void TextBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9')
                && (Keys)e.KeyChar != Keys.Back
                && (Keys)e.KeyChar != Keys.Delete
                && (Keys)e.KeyChar != Keys.Left
                && (Keys)e.KeyChar != Keys.Right
                && Control.ModifierKeys == Keys.None //allow ctrl + V
                )
            {
                e.Handled = true;
            }

            if (TextBoxPort.Text.Length == 5
                && (Keys)e.KeyChar != Keys.Back
                && (Keys)e.KeyChar != Keys.Delete
                && (Keys)e.KeyChar != Keys.Left
                && (Keys)e.KeyChar != Keys.Right  
                )
            {
                e.Handled = true;
            }
        }

    }

    // The ColHeader class is a ColumnHeader object with an
    // added property for determining an ascending or descending sort.
    // True specifies an ascending order, false specifies a descending order.
    //
    public class ColHeader : ColumnHeader
    {
        public bool ascending;
        public ColHeader(string text, int width, HorizontalAlignment align, bool asc)
        {
            this.Text = text;
            this.Width = width;
            this.TextAlign = align;
            this.ascending = asc;
        }

        private void InitializeComponent()
        {

        }
    }
}

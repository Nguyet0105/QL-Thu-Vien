using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTN_QLTV
{
    public partial class DocGia : Form
    {
        public DocGia()
        {
            InitializeComponent();
        }

        private void DocGia_Load(object sender, EventArgs e)
        {
            mobtn();
            dongtxt();
            hien();
        }
        private bool validate()
        {
            if (txtma.Text == "" || txtten.Text == "" || dtngaysinh.Text == "" || txtgioitinh.Text == "" || txtdiachi.Text == "" || txtsdt.Text == "" )
            {
                MessageBox.Show("Bạn phải điền đầy đủ các trường !");
                return false;
            }
            return true;
        }

        private void hien()
        {
            string sql = @"select *from DocGia";

            dtgvdocgia.DataSource = KetNoi.getTable(sql);
        }

        private void binding()
        {
            txtma.DataBindings.Clear();
            txtten.DataBindings.Clear();
            dtngaysinh.DataBindings.Clear();
            txtgioitinh.DataBindings.Clear();
            txtdiachi.DataBindings.Clear();
            txtsdt.DataBindings.Clear();
           

            txtma.DataBindings.Add("Text", dtgvdocgia.DataSource, "MaDocGia");
            txtten.DataBindings.Add("Text", dtgvdocgia.DataSource, "TenDocGia");
            dtngaysinh.DataBindings.Add("Text", dtgvdocgia.DataSource, "NgaySinh");
            txtgioitinh.DataBindings.Add("Text", dtgvdocgia.DataSource, "GioiTinh");
            txtdiachi.DataBindings.Add("Text", dtgvdocgia.DataSource, "DiaChi");
            txtsdt.DataBindings.Add("Text", dtgvdocgia.DataSource, "SDT");
            

        }
        private void motxt()
        {
            txtma.Enabled = true;
                txtten.Enabled = true;
            dtngaysinh.Enabled = true;
            txtdiachi.Enabled = true;
            txtgioitinh.Enabled = true;
            txtsdt.Enabled = true;
           
        }

        private void dongtxt()
        {
            txtma.Enabled = false;
            txtten.Enabled = false;
            dtngaysinh.Enabled = false;
            txtdiachi.Enabled = false;
            txtgioitinh.Enabled = false;
            txtsdt.Enabled = false;
        }

        private void mobtn()
        {
            btluu.Enabled = false;
            bthuy.Enabled = false;
            btthem.Enabled = true;
            btsua.Enabled = true;
            btxoa.Enabled = true;
        }

        private void dongbtn()
        {
            btluu.Enabled = true;
            bthuy.Enabled = true;
            btthem.Enabled = false;
            btsua.Enabled = false;
            btxoa.Enabled = false;
        }

        string state = "";

        private void btthem_Click(object sender, EventArgs e)
        {
            motxt();
            dongbtn();
            state = "insert";
        }

        private void btsua_Click(object sender, EventArgs e)
        {
            motxt();
            dongbtn();
            state = "update";
            binding();
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            motxt();
            dongbtn();
            state = "delete";
            binding();
        }

        private void btluu_Click(object sender, EventArgs e)
        {
            if (validate())
            {



                if (state == "insert")
                {

                    string sql = @"INSERT INTO DocGia (MaDocGia,TenDocGia,NgaySinh,GioiTinh,DiaChi,SDT)
     VALUES
           ('" + txtma.Text + @"'
           ,N'" + txtten.Text + @"'
            ,'" + dtngaysinh.Value.ToString("yyyy/MM/dd") + @"'
            ,N'" + txtgioitinh.Text + @"'
              ,N'" + txtdiachi.Text + @"'
              ," + txtsdt.Text + @"
            )";


                    if (KetNoi.Query(sql) != -1)
                    {
                        MessageBox.Show("Thêm thành công !");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi trong quá trình thêm");
                    }
                }
                //end add
                else if (state == "update")
                {
                    string sql = @"UPDATE [QLTV_LOAN].[dbo].[DocGia]
       SET    
            [TenDocGia]=N'" + txtten.Text + @"'
            ,[NgaySinh]='" + dtngaysinh.Value.ToString("yyyy/MM/dd") + @"'
              ,[GioiTinh]=N'" + txtgioitinh.Text + @"'
             ,[DiaChi]= N'" + txtdiachi.Text + @"'
            ,[SDT]=" + txtsdt.Text + @"
             WHERE MaDocGia = " + txtma.Text;


                    if (KetNoi.Query(sql) != -1)
                    {
                        MessageBox.Show("Sửa thành công !");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi trong quá trình Sửa");
                    }

                }

                hien();
                dongtxt();
                mobtn();
            }//end validate
            if (state == "delete" && validate())
            {
                string sql = @"DELETE DocGia
             WHERE MaDocGia = " + txtma.Text;


                if (KetNoi.Query(sql) != -1)
                {
                    MessageBox.Show("Xóa thành công !");
                }
                else
                {
                    MessageBox.Show("Có lỗi trong quá trình Xóa");
                }

                hien();
                dongtxt();
                mobtn();
            }

        }

        private void bthuy_Click(object sender, EventArgs e)
        {


            mobtn();
            dongtxt();
        }

        private void btthoat_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Visible = true;
        }

        private void bttim_Click(object sender, EventArgs e)
        {
            string sql = @"select* from DocGia";
            DataTable data = KetNoi.getTable(sql);  //lấy dữ liệu từ bảng KhachHang
            if (txttim.Text != "")
            {
                data.DefaultView.RowFilter = "TenDocGia LIKE '%" + txttim.Text + "%' or DiaChi LIKE '%" + txttim.Text + "%' or  GioiTinh LIKE '%" + txttim.Text + "%' ";  //filter lọc dữ liệu
            }
            else
            {
                data.DefaultView.RowFilter = "";
            }

            dtgvdocgia.DataSource = data;
        }

        private void dtgvdocgia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

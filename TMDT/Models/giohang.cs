using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDT.Models;

namespace TMDT.Models
{
    public class giohang
    {
        // đối tượng chứa toàn bộ dữ liệu csdl
        TMDTDBDataContext data = new TMDTDBDataContext();
        public int iidsanpham { set; get; }
        public string stensanpham { set; get; }
        public string shinhanh { set; get; }
        public decimal ddongia { set; get; }
        public int isoluong { set; get; }
        public decimal dthanhtien
        {
            get { return isoluong * ddongia; }
        }
        //khỏi tạo giỏ hàng theo mã sản phẩm được truyền vào (tức khách hàng nhấn nút mua) mặc định là 1
        public giohang(int idsanpham)
        {
            iidsanpham = idsanpham;
            SanPham sp = data.SanPhams.Single(n => n.IDSanPham == idsanpham);
            stensanpham = sp.TenSanPham;
            shinhanh = sp.HinhAnh;
            ddongia = decimal.Parse(sp.DonGia.ToString());
            isoluong = 1;
        }
    }
}

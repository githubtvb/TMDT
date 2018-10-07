using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class giohangController : Controller
    {
        // GET: giohang
        public ActionResult Index()
        {
            return View();
        }
        //đối tượng chứa toàn bộ csdl
        TMDTDBDataContext data = new TMDTDBDataContext();
        //lấy giỏ hàng
        public List<giohang> laygiohang()
        {
            List<giohang> listgiohang = Session["giohang"] as List<giohang>;
            if (listgiohang == null)
            {
                //nếu giỏ hàng null  thì khởi tạo giỏ hàng
                listgiohang = new List<giohang>();
                Session["giohang"] = listgiohang;// mượn biến session để lưu giỏ hàng tạm thời
            }
            return listgiohang;
        }
        //tạo phương thức thêm sản phẩm vào giỏ hàng
        public ActionResult themgiohang(int iidsanpham, string strurl)
        {
            //lấy session giỏ hàng
            List<giohang> listgiohang = laygiohang();
            //kiểm tra sản phẩm đã có trong giỏ hàng chưa.?
            giohang sanpham = listgiohang.Find(n => n.iidsanpham == iidsanpham);
            if (sanpham == null)
            {
                sanpham = new giohang(iidsanpham);
                listgiohang.Add(sanpham);
                return Redirect(strurl);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strurl);
            }
        }
        // phương thức tính tổng sô lượng
        private int tongsoluong()
        {
            int itongsoluong = 0;
            List<giohang> listgiohang = Session["giohang"] as List<giohang>;
            if (listgiohang != null)
            {
                itongsoluong = listgiohang.Sum(n => n.isoluong);
            }
            return itongsoluong;
        }
        // phương thức tính tổng tiền cho giỏ hàng
        private decimal tongtien()
        {
            decimal itongtien = 0;
            List<giohang> listgiohang = Session["giohang"] as List<giohang>;
            if (listgiohang != null)
            {
                itongtien = listgiohang.Sum(n => n.dthanhtien);
            }
            return itongtien;
        }
        // xây dựng trang giỏ hàng và hiển thị
        public ActionResult giohang()
        {
            List<giohang> listgiohang = laygiohang();
            if (listgiohang.Count == 0)
            {
                Response.Write("<script>alert('bạn không có sản phẩm nào trong giỏ hàng hãy đặt mua ngay!')</script>");
                
                return RedirectToAction("index", "NguoiDung");

            }
            ViewBag.tongsoluong = tongsoluong();
            ViewBag.tongtien = tongtien();
            return View(listgiohang);
        }
        // xóa sản phẩm trong giỏ hàng
        public ActionResult xoagiohang(int iidsanpham)
        {
            //lấy giỏ hàng từ session
            List<giohang> listgiohang = laygiohang();
            //kiểm tra sản phẩm đã có trong session
            giohang sanpham = listgiohang.SingleOrDefault(n => n.iidsanpham == iidsanpham);
            // nếu tồn tại thì cho sửa
            if (sanpham !=null)
            {
                listgiohang.RemoveAll(n => n.iidsanpham == iidsanpham);
                return RedirectToAction("giohang");

            }
            if (listgiohang.Count==0)
            {
                return RedirectToAction("index","TMDT");
            }
            return RedirectToAction("giohang");
        }
        //cập nhật giỏ hàng
        public ActionResult capnhatgiohang(int iidsanpham,FormCollection f)
        {
            // lấy giỏ hàng từ session
            List<giohang> listgiohang = laygiohang();
            //kiểm tra sản phẩm ,y/n?
            giohang sanpham = listgiohang.SingleOrDefault(n=>n.iidsanpham==iidsanpham);
            //nếu tồn tại cho phép sửa
            if (sanpham !=null)
            {
                sanpham.isoluong = int.Parse(f["txtsoluong"].ToString());
                return RedirectToAction("giohang");
            }
        }
        //xóa tất cả sản phẩm, giohang=null
        public ActionResult xoatatcagiohang()
        {
            //lấy gio hàng
            List<giohang> listgiohang = laygiohang();
            listgiohang.Clear();
            return RedirectToAction("index","TMDT");
        }
    }
}

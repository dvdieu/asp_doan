using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.Models;
using System.IO;
using DoAnWeb.Areas.Admin.Filter;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [CheckLoginAdmin]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        public ActionResult Add()
        {
            Poco_Page_ThemSanPham item = new Poco_Page_ThemSanPham();
            using (ModelEntities ctx = new ModelEntities())
            {
                item.DanhSachLoaiSanPham = ctx.tbl_LoaiSanPhams.ToList();
                item.DanhSachNhaCungCap = ctx.tbl_NhaSanXuats.ToList();
                return View(item);
            }
        }

        [HttpPost]
        // POST: Admin/Products
        [ValidateInput(false)]
        public ActionResult Add(tbl_SanPhams model,HttpPostedFileBase image)
        {
            Poco_Page_ThemSanPham item = new Poco_Page_ThemSanPham();
            if (string.IsNullOrEmpty(model.MoTaDai)) model.MoTaDai = string.Empty;
            if (string.IsNullOrEmpty(model.MoTaNgan)) model.MoTaNgan = string.Empty;
            model.DaXoa = false;
            model.SoLanXem = 0;
            model.SoLanMua = 0;
            using (ModelEntities ctx = new ModelEntities())
            {
                ctx.tbl_SanPhams.Add(model);
                ctx.SaveChanges();
                item.DanhSachLoaiSanPham = ctx.tbl_LoaiSanPhams.ToList();
                item.DanhSachNhaCungCap = ctx.tbl_NhaSanXuats.ToList();
                if(image !=null && image.ContentLength >0)
                {
                    //Tao folder chua hinh
                    string patch1 = Server.MapPath("~/image");
                    string tagetdir = Path.Combine(patch1, model.SanPhamID.ToString());
                    Directory.CreateDirectory(tagetdir);
                    //Copy hinh
                    string mainThumn = Path.Combine(tagetdir, "main_thumn.jpg");
                    image.SaveAs(mainThumn);
                }
                return View(item);
            }
        }
        // GET: Admin/Products
        public ActionResult Index()
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                return View(ctx.tbl_SanPhams.Where(p=>p.DaXoa==false).ToList());
            }
        }

        // GET: Admin/Edit
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Poco_Page_SuaSanPham  item = new Poco_Page_SuaSanPham();
            using (ModelEntities ctx = new ModelEntities())
            {
                item.SanPham = ctx.tbl_SanPhams.Where(p => p.SanPhamID == id.Value).FirstOrDefault();
                if(item.SanPham ==null)
                {
                    return RedirectToAction("Index");
                }
                item.DanhSachLoaiSanPham = ctx.tbl_LoaiSanPhams.ToList();
                item.DanhSachNhaSanXuat = ctx.tbl_NhaSanXuats.ToList();
                
                
                return View(item);
            }
        }
        // GET: Admin/Edit
        [HttpPost]
        //[MultipleButton(Name ="action",Argument = "Edit")]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_SanPhams item,HttpPostedFileBase image)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_SanPhams itemFind = ctx.tbl_SanPhams.Where(p => p.SanPhamID == item.SanPhamID).FirstOrDefault();
                if(itemFind ==null)
                {
                    return RedirectToAction("Index");
                }
                itemFind.TenSanPham = item.TenSanPham;
                itemFind.Gia = item.Gia;
                itemFind.MoTaNgan = item.MoTaNgan;
                itemFind.MoTaDai = item.MoTaDai;
                itemFind.NongDo = item.NongDo;
                itemFind.SoLuong = item.SoLuong;
                itemFind.TinhTrang = item.TinhTrang;
                itemFind.LoaiSanPhamID = item.LoaiSanPhamID;
                itemFind.NhaSanXuatID = item.NhaSanXuatID;
                if (image != null && image.ContentLength > 0)
                {
                    //Tao folder chua hinh
                    string patch1 = Server.MapPath("~/image");
                    string tagetdir = Path.Combine(patch1, item.SanPhamID.ToString());
                    Directory.CreateDirectory(tagetdir);
                    //Copy hinh
                    string mainThumn = Path.Combine(tagetdir, "main_thumn.jpg");
                    image.SaveAs(mainThumn);
                }
                ctx.SaveChanges();
                return RedirectToAction("Edit", new { id = item.SanPhamID });
            }
        }
        [HttpPost]
        //[MultipleButton(Name = "action", Argument = "Delete")]
        public ActionResult Delete(int? id)
        {
            using (ModelEntities ctx = new ModelEntities())
            {
                tbl_SanPhams itemFind = ctx.tbl_SanPhams.Where(p => p.SanPhamID == id).FirstOrDefault();
                itemFind.DaXoa = true;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
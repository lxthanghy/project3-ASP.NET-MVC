using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using DTO;
using LXTHANG_10117004.Areas.Admin.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        private SupplierBLL supplierBLL = new SupplierBLL();
        // GET: Admin/Supplier
        public ActionResult Index(int? page, int? size, bool? loadPage, string searchName)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 5;
            bool LoadPage = loadPage ?? false;
            ViewBag.PageSize = PageSize;
            ViewBag.LoadPage = LoadPage;
            ViewBag.SearchName = string.IsNullOrEmpty(searchName) ? "" : searchName;
            List<Supplier> model = null;
            if (loadPage == true || Session["SS_SUPPLIER"] == null)
            {
                LoadPage = false;
                ViewBag.LoadPage = LoadPage;
                model = supplierBLL.GetAll();
                Session["SS_SUPPLIER"] = model;
            }
            else
            {
                model = (List<Supplier>)Session["SS_SUPPLIER"];
            }
            if (!string.IsNullOrEmpty(searchName))
                model = model.Where(x => (x.Name.ToLower().Contains(searchName.ToLower()))).ToList();
            ViewBag.Total = model.Count;
            ViewBag.Page = PageNumber;
            return View(model.ToPagedList(PageNumber, PageSize));
        }
        [HttpPost]
        public JsonResult SaveSupplier(string supplier)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Supplier sl = serializer.Deserialize<Supplier>(supplier);
            try
            {
                if (sl != null)
                {
                    if (sl.ID == 0)
                        supplierBLL.AddSupplier(sl);
                    else
                        supplierBLL.EditSupplier(sl);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public JsonResult GetById(int id)
        {
            try
            {
                var supp = supplierBLL.GetById(id);
                var x = new SupplierViewModel();
                x.ID = supp.ID;
                x.Name = supp.Name;
                x.Address = supp.Address;
                x.Email = supp.Email;
                x.Mobile = supp.Mobile;
                x.CreatedBy = supp.CreatedBy;
                x.CreatedDate = supp.CreatedDate.ToString();
                x.ModifiedBy = supp.ModifiedBy;
                x.ModifiedDate = supp.ModifiedDate.ToString();
                x.Status = supp.Status;
                return Json(x, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteSupplier(int id)
        {
            var result = supplierBLL.DeleteSupplier(id);
            return Json(result);
        }
        public FileResult ExportExcel(int pageSize)
        {
            //FileInfo file = new FileInfo(Server.MapPath(@"/Template/template.xlsx"));
            ExcelPackage pack = new ExcelPackage();
            ExcelWorkbook book = pack.Workbook;
            var model = (List<Supplier>)Session["SS_SUPPLIER"];
            double a = ((double)model.Count / pageSize);
            double b = Math.Ceiling(a);
            int sheets = Convert.ToInt32(b.ToString());
            for (int i = 1; i <= sheets; i++)
            {
                ExcelWorksheet worksheet = pack.Workbook.Worksheets.Add("Trang" + i);
            }
            int page = 1;
            int skip = 0;
            do
            {
                var data = model.Skip(skip).Take(pageSize).ToList();
                ExcelWorksheet sheet = book.Worksheets["Trang" + page];
                sheet.Cells["A1:E1"].Merge = true;
                sheet.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:E1"].Style.Font.Bold = true;
                sheet.Cells["A1:E1"].Style.Font.Size = 15;
                sheet.Cells["A1:E1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A1:E1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A1:E1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A1:E1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
                sheet.Cells["A1"].Value = "Danh sách nhà cung cấp";
                sheet.TabColor = System.Drawing.Color.Black;
                sheet.DefaultRowHeight = 12;
                sheet.Row(2).Height = 20;
                sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Row(2).Style.Font.Bold = true;
                sheet.Cells[2, 1].Value = "STT";
                sheet.Cells[2, 2].Value = "Tên";
                sheet.Cells[2, 3].Value = "Địa chỉ";
                sheet.Cells[2, 4].Value = "Email";
                sheet.Cells[2, 5].Value = "Điện thoại";
                sheet.Cells[2, 6].Value = "Người tạo";
                sheet.Cells[2, 7].Value = "Ngày tạo";
                sheet.Cells[2, 8].Value = "Người sửa";
                sheet.Cells[2, 9].Value = "Ngày sửa";
                int row = 3;
                for (int i = 0; i < data.Count; i++)
                {
                    sheet.Cells[row, 1].Value = (i + 1).ToString();
                    sheet.Cells[row, 2].Value = data[i].Name;
                    sheet.Cells[row, 3].Value = data[i].Address;
                    sheet.Cells[row, 4].Value = data[i].Email;
                    sheet.Cells[row, 5].Value = data[i].Mobile;
                    sheet.Cells[row, 6].Value = string.IsNullOrEmpty(data[i].CreatedBy) ? "N/A" : data[i].CreatedBy;
                    sheet.Cells[row, 7].Value = data[i].CreatedDate.ToString();
                    sheet.Cells[row, 8].Value = string.IsNullOrEmpty(data[i].ModifiedBy) ? "N/A" : data[i].ModifiedBy;
                    sheet.Cells[row, 9].Value = data[i].ModifiedDate.ToString();
                    row++;
                }
                skip += pageSize;
                page++;
            }
            while (page <= sheets);
            string fileName = string.Concat("Supplier", DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
            return File(pack.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
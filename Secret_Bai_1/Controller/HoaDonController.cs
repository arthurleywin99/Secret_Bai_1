using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secret_Bai_1.Model;

namespace Secret_Bai_1.Controller
{
    class HoaDonController
    {
        public static List<HoaDon> GetAllHoaDon()
        {
            using (var context = new Context())
            {
                return context.HoaDons.ToList();
            }
        }
        public static bool AddHoaDon(HoaDon hoaDon, out string error)
        {
            using (var context = new Context())
            {
                error = string.Empty;
                try
                {
                    context.HoaDons.Add(hoaDon);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
        }

        public static bool AddChiTietHoaDon(ChiTietHoaDon chiTietHoaDon, out string error)
        {
            using (var context = new Context())
            {
                error = string.Empty;
                try
                {
                    context.ChiTietHoaDons.Add(chiTietHoaDon);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                    return false;
                }
            }
        }
    }
}

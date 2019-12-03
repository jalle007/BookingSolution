using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using OfficeOpenXml;
using BookingApi.Models;
using System.Globalization;

namespace BookingAPI.Providers
{
    public class ExcelProvider
    {
        private readonly BookingContext _context;

        public ExcelProvider(BookingContext context) { _context = context; }

        public bool ParseData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("CS-03450-2019.xlsx"));


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[1];

                    try
                    {

                        // Parse names
                        for (int row = 3; row <= 7; row++)
                        {
                            int id = Convert.ToInt32(worksheet.Cells[$"K{row}"].Value.ToString());
                            string name = worksheet.Cells[$"L{row}"].Value?.ToString();
                            string city = worksheet.Cells[$"M{row}"].Value?.ToString();
                            string phone = worksheet.Cells[$"N{row}"].Value?.ToString();
                            string email = worksheet.Cells[$"O{row}"].Value?.ToString();
                            string contract_number = worksheet.Cells[$"P{row}"].Value?.ToString();

                            var cap = new CapacityProvider()
                            {
                                id = id,
                                name = name,
                                city = city,
                                phone = phone,
                                email = email,
                                contract_number = contract_number
                            };
                            _context.CapacityProviders.Add(cap);
                            _context.SaveChanges();
                        }

                        // Parse bookings
                        for (int row = 3; row <= 15; row++)
                        {
                            int id = Convert.ToInt32(worksheet.Cells[$"A{row}"].Value.ToString());
                            string object_name = worksheet.Cells[$"B{row}"].Value?.ToString();
                            int CapacityProviderId = Convert.ToInt32(worksheet.Cells[$"C{row}"].Value?.ToString());
                            DateTime date_from =Convert.ToDateTime( worksheet.Cells[$"D{row}"].Value?.ToString());
                            DateTime date_to =Convert.ToDateTime( worksheet.Cells[$"E{row}"].Value?.ToString());
                            bool is_active =Convert.ToBoolean( worksheet.Cells[$"F{row}"].Value?.ToString());
                            float amount = float.Parse(worksheet.Cells[$"G{row}"].Value?.ToString(), CultureInfo.InvariantCulture);
                            string currency = worksheet.Cells[$"H{row}"].Value?.ToString();
                            string comment = worksheet.Cells[$"I{row}"].Value?.ToString();


                            var booking = new Booking()
                            { 
                                id = id,
                                object_name = object_name,
                                CapacityProviderId = CapacityProviderId,
                                date_from = date_from,
                                date_to = date_to,
                                is_active = is_active,
                                amount = amount,
                                currency = currency,
                                comment = comment
                            };
                            _context.Bookings.Add(booking);
                            _context.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                  
                }
            }

            return true;

        }
    }


}

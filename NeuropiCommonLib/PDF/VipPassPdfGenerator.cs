using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace NeuropiCommonLib.PDF
{
    public class VipPassPdfGenerator
    {
        public static byte[] GenerateVipPassesPdf(string vipName, List<(int PassId, byte[] QrCodeBytes)> passes)
        {
            // Set QuestPDF license
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                foreach (var pass in passes)
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(40);
                        
                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("VIP ACCESS PASSES").FontSize(24).Bold().FontColor(Colors.Red.Darken4);
                                col.Item().Text("Carpe Diem Celebration 2026").FontSize(14).SemiBold().FontColor(Colors.Grey.Darken2);
                            });
                            
                            row.ConstantItem(80).AlignCenter().Text("VIP").FontSize(32).Bold().FontColor(Colors.Red.Lighten4);
                        });

                        page.Content().PaddingVertical(20).Column(col =>
                        {
                            col.Spacing(30);
                            
                            col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingBottom(10).Column(venueCol =>
                            {
                                venueCol.Spacing(5);
                                venueCol.Item().Row(row => 
                                {
                                    row.RelativeItem().Text(text =>
                                    {
                                        text.Span("Honored Guest: ").SemiBold().FontSize(14);
                                        text.Span(vipName).FontSize(14);
                                    });
                                    
                                    row.RelativeItem().AlignRight().Text(text =>
                                    {
                                        text.Span("Pass ID: ").SemiBold();
                                        text.Span(pass.PassId.ToString());
                                    });
                                });

                                venueCol.Item().Row(row =>
                                {
                                    row.RelativeItem().Text(text =>
                                    {
                                        text.Span("Venue: ").SemiBold();
                                        text.Span("Ashray Conventions, Near Hitech city Metro, Hyderabad");
                                    });
                                });

                                venueCol.Item().Row(row =>
                                {
                                    row.RelativeItem().Text(text =>
                                    {
                                        text.Span("Location: ").SemiBold().FontColor(Colors.Red.Darken4);
                                        text.Span("https://maps.app.goo.gl/twAGzoyvyfzfCdCj7").FontColor(Colors.Blue.Medium);
                                    });
                                });
                            });

                            col.Item().AlignCenter().Column(passCol =>
                            {
                                passCol.Spacing(20);
                                passCol.Item().AlignCenter().Text($"VIP Entry Pass").Bold().FontSize(20).FontColor(Colors.Red.Darken3);
                                
                                passCol.Item().AlignCenter().Width(300).Height(300).Image(pass.QrCodeBytes);
                                
                                passCol.Item().AlignCenter().Text("Valid for One Admission Only").FontSize(12).SemiBold();
                                passCol.Item().AlignCenter().PaddingTop(10).Text("Please present this QR code at the VIP Entrance for verification.").FontSize(10).Italic().FontColor(Colors.Grey.Medium);
                            });
                        });

                        page.Footer().AlignCenter().Text(text =>
                        {
                            text.Span("© 2026 My School Italy & Neuropi Ai").FontSize(9).FontColor(Colors.Grey.Medium);
                        });
                    });
                }
            });

            return document.GeneratePdf();
        }
    }
}

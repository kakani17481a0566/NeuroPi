using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace NeuropiCommonLib.PDF
{
    public class VipPassPdfGenerator
    {
        // Define Brand Colors
        private static readonly string BrandNavy = "#00204a";
        private static readonly string BrandGold = "#d4af37";
        private static readonly string BrandLightGrey = "#f0f2f5";
        private static readonly string TextDark = "#444444";

        public static byte[] GenerateVipPassesPdf(string vipName, List<(int PassId, byte[] QrCodeBytes)> passes)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                foreach (var pass in passes)
                {
                    container.Page(page =>
                    {
                        // 1. Page Background (Light Grey to make the white card pop)
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.PageColor(BrandLightGrey);
                        page.DefaultTextStyle(x => x.FontFamily(Fonts.Arial).Color(TextDark));

                        page.Content().AlignCenter().Column(col =>
                        {
                            // Centering the "Card" vertically roughly
                            col.Spacing(20);

                            // 2. The Main Card Container
                            col.Item().Background(Colors.White).BorderTop(5).BorderColor(BrandGold).Column(card =>
                            {
                                // --- HEADER SECTION (Deep Navy) ---
                                card.Item().Background(BrandNavy).PaddingVertical(20).PaddingHorizontal(20).Column(header =>
                                {
                                    header.Spacing(5);

                                    // Small Gold Top Label
                                    header.Item().AlignCenter().Text(t => t.Span("MY SCHOOL ITALY & NEUROPI AI")
                                          .FontColor(BrandGold).FontSize(10).SemiBold().LetterSpacing(0.1f));

                                    // Main Title (Serif Font for Premium look)
                                    header.Item().AlignCenter().Text(t => t.Span("VIP INVITATION")
                                          .FontFamily(Fonts.TimesNewRoman) // Uses System Times New Roman
                                          .FontColor(Colors.White).FontSize(28));

                                    // Subtitle
                                    header.Item().AlignCenter().Text(t => t.Span("Carpe Diem Celebration 2026")
                                          .FontColor(Colors.Grey.Lighten2).FontSize(14).LetterSpacing(0.05f));
                                });

                                // --- BODY SECTION ---
                                card.Item().Padding(20).Column(body =>
                                {
                                    body.Spacing(15);

                                    // Greeting
                                    body.Item().AlignCenter().Text(t => t.Span("Honored Guest")
                                        .FontFamily(Fonts.TimesNewRoman).FontSize(22).FontColor(BrandNavy));

                                    body.Item().AlignCenter().Text(t => t.Span("WE CORDIALLY INVITE YOU TO BE OUR SPECIAL GUEST FOR CarpeDiem 2026. OUR CHILDREN WILL BE PERFORMING AMAZING AND ASTONISHING DANCE, DRAMA, GYMNASTICS, AND SHOWCASING THEIR TABLE TENNIS SKILLS. IT PROMISES TO BE A JOYFUL CELEBRATION OF THE TALENT AND HARD WORK OF OUR STUDENTS AND STAFF!")
                                        .FontSize(11).LineHeight(1.5f).FontColor(Colors.Grey.Darken2));

                                    // --- DETAILS BOX (Grey box with Gold border) ---
                                    body.Item().Background(Colors.Grey.Lighten4)
                                        .BorderLeft(4).BorderColor(BrandGold)
                                        .Padding(15)
                                        .Row(row =>
                                        {
                                            // Left Col: Date
                                            row.RelativeItem().Column(d =>
                                            {
                                                d.Item().Text(t => t.Span("DATE & TIME").FontSize(9).SemiBold().FontColor(Colors.Grey.Darken1));
                                                d.Item().Text(t => t.Span("28TH FEB 2026").FontSize(14).Bold().FontColor(BrandNavy));
                                                d.Item().Text(t => t.Span("10:30 AM TO 01:30 PM").FontSize(12).Bold().FontColor(BrandNavy));
                                            });

                                            // Right Col: Venue
                                            row.RelativeItem().Column(v =>
                                            {
                                                v.Item().Text(t => t.Span("VENUE").FontSize(9).SemiBold().FontColor(Colors.Grey.Darken1));
                                                v.Item().Text(t => t.Span("Ashray Conventions").FontSize(14).Bold().FontColor(BrandNavy));
                                                v.Item().Text(t => t.Span("Near Hitech City Metro, Hyderabad").FontSize(10));
                                            });
                                        });

                                    // TEA & LUNCH Box
                                    body.Item().Background(Colors.Grey.Lighten4)
                                        .BorderLeft(4).BorderColor(BrandGold)
                                        .Padding(15)
                                        .Column(c =>
                                        {
                                            c.Item().Text(t => t.Span("TEA & LUNCH").FontSize(9).SemiBold().FontColor(Colors.Grey.Darken1));
                                            c.Item().Text(t => t.Span("TEA - 11:00 AM TO 12:30 PM | LUNCH - 12:45 PM TO 02:45 PM").FontSize(11).Bold().FontColor(BrandNavy));
                                        });

                                    // Map Link Button style
                                    body.Item().AlignCenter().PaddingTop(10).Element(ComposeMapLink);

                                    // --- QR CODE SECTION ---
                                    body.Item().PaddingTop(10).Column(qr =>
                                    {
                                        qr.Spacing(5);
                                        qr.Item().AlignCenter().Width(100).Height(100).Image(pass.QrCodeBytes);

                                        qr.Item().AlignCenter().Text(text =>
                                        {
                                            text.Span("PASS ID: ").FontSize(10).SemiBold();
                                            text.Span(pass.PassId.ToString()).FontSize(10);
                                        });

                                        qr.Item().AlignCenter().Text(t => t.Span("Please present this at the VIP Entrance.")
                                           .FontSize(10).Italic().FontColor(Colors.Grey.Medium));
                                    });
                                });

                                // --- FOOTER SECTION ---
                                card.Item().Background(Colors.Grey.Lighten4)
                                    .BorderTop(1).BorderColor(Colors.Grey.Lighten3)
                                    .Padding(10)
                                    .AlignCenter()
                                    .Text(t => t.Span($"© 2026 My School Italy & Neuropi Ai").FontSize(10).FontColor(Colors.Grey.Darken1));
                            });
                        });
                    });
                }
            });

            return document.GeneratePdf();
        }

        // Helper for the button style
        static void ComposeMapLink(IContainer container)
        {
            container
                .Background(BrandNavy)
                .PaddingVertical(10)
                .PaddingHorizontal(25)
                .Hyperlink("https://maps.app.goo.gl/twAGzoyvyfzfCdCj7")
                .Text(t => t.Span("VIEW LOCATION MAP")
                    .FontColor(BrandGold)
                    .FontSize(10)
                    .Bold());
        }
    }
}
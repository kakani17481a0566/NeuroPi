using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace NeuropiCommonLib.PDF
{
    public class NdaPdfGenerator
    {
        public static byte[] GenerateNdaPdf(
            string companyName,
            string contactPerson,
            string email,
            string contactNumber,
            string place,
            DateTime agreedOn,
            string recipientSignatureBase64,
            string neuroPiSignaturePath)
        {
            // Set QuestPDF license (Community license is free for open-source)
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);

                    page.Content().Element(content => ComposeContent(
                        content,
                        companyName,
                        contactPerson,
                        email,
                        contactNumber,
                        place,
                        agreedOn,
                        recipientSignatureBase64,
                        neuroPiSignaturePath));

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("© 2025 NeuroPi Tech Private Limited. All rights reserved.")
                            .FontSize(9).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            return document.GeneratePdf();
        }

        private static void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().AlignCenter().Text("NeuroPi Tech Private Limited")
                    .FontSize(18).Bold().FontColor(Colors.Blue.Darken3);

                column.Item().AlignCenter().Text("Non-Disclosure and Confidentiality Agreement")
                    .FontSize(14).SemiBold();

                column.Item().PaddingTop(5).LineHorizontal(2).LineColor(Colors.Grey.Lighten1);
            });
        }

        private static void ComposeContent(
            IContainer container,
            string companyName,
            string contactPerson,
            string email,
            string contactNumber,
            string place,
            DateTime agreedOn,
            string recipientSignatureBase64,
            string neuroPiSignaturePath)
        {
            var displayDate = agreedOn.ToString("MMMM dd, yyyy");

            container.Column(column =>
            {
                column.Spacing(10);

                // Introduction
                column.Item().Text(text =>
                {
                    text.Span("This Non-Disclosure Agreement (the \"Agreement\") is made on this ");
                    text.Span(displayDate).Bold();
                    text.Span(", between ");
                    text.Span("NeuroPi Tech Private Limited").Bold();
                    text.Span(" and ");
                    text.Span(companyName).Bold();
                    text.Span(". The signatories referred to herein individually as a \"Party\" or collectively as the \"Parties\". For good and valuable consideration, the receipt of sufficiency of which each of the Parties hereto acknowledge, the Parties do hereby agree as follows:");
                });

                // Section 1: Purpose
                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("1. Purpose: ").Bold();
                    text.Span("The Parties wish to explore scope of discussions, evaluations, and potential collaboration, investment, partnership or advisory relating to NeuroPi's business, products, technologies, and services across all its verticals.");
                });

                // Section 2: Confidential Information (Summarized for PDF)
                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("2. Confidential Information: ").Bold();
                    text.Span("For the purposes of this Agreement, \"Confidential Information\" shall mean all non-public, proprietary, or sensitive information disclosed by NeuroPi to the Receiving Party, including but not limited to:");
                });

                column.Item().PaddingLeft(20).Text("• Business models, strategies, roadmaps, and go-to-market plans\n" +
                    "• Pitch decks, executive summaries, financials, projections, valuations\n" +
                    "• Intellectual property, algorithms, software, AI/ML models, data architectures\n" +
                    "• Neuroscience, genetics, epigenetics, biomarker frameworks\n" +
                    "• Curriculum design, pedagogy, training frameworks, educational IP\n" +
                    "• Sports performance, defence readiness, and human optimization programs\n" +
                    "• Longevity, wellness, preventive health frameworks\n" +
                    "• Proprietary datasets, assessments, reports, dashboards, DNA-informed insights\n" +
                    "• Client, partner, vendor, government, or defence-related information").FontSize(10);

                // Additional clauses (summarized)
                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("This Agreement includes additional provisions regarding exclusions, non-use and non-disclosure, maintenance of confidentiality, governing law (India), jurisdiction (Hyderabad, Telangana), dispute resolution, term (5 years), and other standard NDA clauses. Full details are available in the digital version.");
                });

                // Signatures Section
                column.Item().PaddingTop(20).LineHorizontal(2).LineColor(Colors.Grey.Lighten1);

                column.Item().PaddingTop(15).Row(row =>
                {
                    // NeuroPi Signature Block
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("For NeuroPi Tech Private Limited").Bold();
                        col.Item().Text("(DISCLOSING PARTY)").FontSize(9).Italic();

                        col.Item().PaddingTop(10).Text("SIGNATURE").FontSize(8).Bold();

                        if (File.Exists(neuroPiSignaturePath))
                        {
                            col.Item().PaddingTop(5).Height(60).Image(neuroPiSignaturePath);
                        }

                        col.Item().PaddingTop(10).Text("DR. APARNA RAO VOLLURU").Bold();
                        col.Item().Text("Founder & CEO, NeuroPi Tech Pvt. Ltd.").FontSize(10);
                        col.Item().PaddingTop(5).Text($"Date: {displayDate}").FontSize(10);
                        col.Item().Text("Place: Hyderabad, INDIA").FontSize(10);
                    });

                    // Spacing
                    row.ConstantItem(30);

                    // Recipient Signature Block
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text($"For {companyName}").Bold();
                        col.Item().Text("(RECEIVING PARTY)").FontSize(9).Italic();

                        col.Item().PaddingTop(10).Text("SIGNATURE").FontSize(8).Bold();

                        if (!string.IsNullOrEmpty(recipientSignatureBase64))
                        {
                            try
                            {
                                // Convert base64 to bytes
                                var base64Data = recipientSignatureBase64.Contains(",")
                                    ? recipientSignatureBase64.Split(',')[1]
                                    : recipientSignatureBase64;

                                var imageBytes = Convert.FromBase64String(base64Data);
                                col.Item().PaddingTop(5).Height(60).Image(imageBytes);
                            }
                            catch
                            {
                                col.Item().PaddingTop(5).Text("[Signature Error]").FontSize(8).Italic();
                            }
                        }

                        col.Item().PaddingTop(10).Text(contactPerson).Bold();
                        col.Item().Text($"Email: {email}").FontSize(10);
                        if (!string.IsNullOrEmpty(contactNumber))
                        {
                            col.Item().Text($"Contact: {contactNumber}").FontSize(10);
                        }
                        col.Item().PaddingTop(5).Text($"Date: {displayDate}").FontSize(10);
                        if (!string.IsNullOrEmpty(place))
                        {
                            col.Item().Text($"Place: {place}").FontSize(10);
                        }
                    });
                });
            });
        }
    }
}

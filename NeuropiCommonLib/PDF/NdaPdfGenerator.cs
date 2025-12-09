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
                // Section 2: Confidential Information
                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("2. Confidential Information: ").Bold();
                    text.Span("For the purposes of this Agreement, \"Confidential Information\" shall mean all non-public, proprietary, or sensitive information disclosed by NeuroPi to the Receiving Party, whether orally, visually, electronically, or in writing, including but not limited to:");
                });

                column.Item().PaddingLeft(20).Text("• Business models, strategies, roadmaps, and go-to-market plans\n" +
                    "• Pitch decks, executive summaries, financials, projections, valuations, and funding plans\n" +
                    "• Intellectual property, algorithms, software, AI/ML models, data architectures, and system designs\n" +
                    "• Neuroscience, genetics, epigenetics, biomarker frameworks, and applied research methodologies\n" +
                    "• Curriculum design, pedagogy, training frameworks, school systems, and educational IP\n" +
                    "• Sports performance, defence readiness, and human optimization programs\n" +
                    "• Longevity, wellness, preventive health, and compatibility assessment frameworks\n" +
                    "• Proprietary datasets, assessments, reports, dashboards, DNA-informed insights, and analytics\n" +
                    "• Client, partner, vendor, government, or defence-related information\n" +
                    "• Any information marked or reasonably understood to be confidential").FontSize(10);
                
                column.Item().PaddingTop(5).Text("Confidential Information shall include without limitation, business and financial information about costs, profits, markets, sales, customers and bids, business plans, marketing, future developments, product developments and new products concepts and technical information, Schematics, techniques, suggestions, development tools and processes, computer programs, designs, manuals, electronic codes, software demonstration programs, computer systems, documentation, procedures, ideas, know-how, inventions (whether patentable or not), improvements, concepts, records, files, memoranda, reports, plans, price lists, customer lists, forecasts, strategies, any apparatus, modules, samples, prototypes or parts thereof or any, consultant(s) and representative(s) list, employee(s) list and all document, books, papers, model, and other data of any kind and descriptions, including electronic data recorded or retrieved by any means, that have been or will be given to the Recipient by the Discloser, as well as written or verbal instructions or comments and the like. Where any of the above information is given verbally, the Discloser shall provide a summary of the information to the Recipient in writing within thirty (30) days by way of confirmation that the same is subject to the terms of this Agreement. For the purposes of this section, profiles/details of employees shared if any under this Agreement, shall be considered as Confidential Information.");

                // Clauses 3-32
                AddClause(column, "3. Exclusions from Confidential Information", "The obligations of either party under this Section will not apply to information that the receiving party can demonstrate: (i) was in its possession at the time of disclosure and without restriction as to confidentiality, (ii) at the time of disclosure is generally available to the public or after disclosure becomes generally available to the public through no breach of agreement or other wrongful act by the receiving party, (iii) has been received from a third party without restriction on disclosure and without breach of agreement by the receiving party, (iv) is independently developed by the receiving party without regard to the Confidential Information of the other party, or (v) is required to be disclosed by law or order of a court of competent jurisdiction or regulatory authority, provided that the receiving party shall furnish prompt written notice of such required disclosure and reasonably cooperate with the disclosing party, at the disclosing party's expense, in any effort made by the disclosing party to seek a protective order or other appropriate protection of its Confidential Information.");
                
                AddClause(column, "4. Non-Use and Non-Disclosure", "The Recipient agrees not to use any Confidential Information for any purpose except to evaluate and engage in discussions concerning potential business relationship between the parties. Recipient agrees not to disclose any Confidential Information to third parties or to employees of the Recipient, except to those employees of the Recipient who are required to have the information in order to evaluate or engage in discussions concerning the contemplated business relationship.");

                AddClause(column, "5. Maintenance of Confidentiality", "The Recipient shall take all reasonable measures to protect the secrecy of and avoid disclosure or use of Confidential Information of the Discloser in order to prevent it from falling into the public domain or the possession of persons other than those persons authorized under this Agreement to have any such information. Such measures shall include the highest degree of care that the Recipient utilizes to protect its own Confidential Information of a similar nature, which shall be no less than reasonable care.");

                AddClause(column, "6. No Copying", "The Recipient shall not copy, duplicate, reverse engineer, reverse compile, disassemble, record, or otherwise reproduce any part of the Confidential Information without the prior written consent of the Discloser, except as reasonably required for the Purpose.");

                AddClause(column, "7. No License", "Nothing in this Agreement is intended to grant any rights to the Recipient under any patent, mask work right or copyright of the Discloser, nor shall this Agreement grant the Recipient any rights in or to the Confidential Information of the Discloser except as expressly set forth herein.");

                AddClause(column, "8. Term", "The foregoing commitments of each party shall survive any termination of the relationship between the parties and shall continue for a period of five (5) years following the date of this Agreement.");

                AddClause(column, "9. Return of Materials", "Any materials or documents that have been furnished by the Discloser to the Recipient in connection with the relationship of the parties shall be promptly returned by the Recipient, accompanied by all copies of such documentation, within ten (10) days after the relationship has terminated or at the written request of the Discloser.");

                AddClause(column, "10. No Warranty", "The Discloser warrants that it has the right to disclose the Confidential Information regarding the Purpose. However, ALL CONFIDENTIAL INFORMATION IS PROVIDED \"AS IS\". THE DISCLOSER MAKES NO WARRANTIES, EXPRESS, IMPLIED OR OTHERWISE, REGARDING ITS ACCURACY, COMPLETENESS OR PERFORMANCE.");

                AddClause(column, "11. Independent Development", "This Agreement shall not be construed to limit either party's right to independently develop or acquire products without use of the other party's Confidential Information. The Discloser acknowledges that the Recipient may currently or in the future be developing information internally, or receiving information from other parties, that is similar to the Confidential Information.");

                AddClause(column, "12. Remedies", "The Recipient agrees that any violation or threatened violation of this Agreement may cause irreparable injury to the Discloser, entitling the Discloser to seek injunctive relief in addition to all legal remedies.");

                AddClause(column, "13. Governing Law", "This Agreement and all acts and transactions pursuant hereto and the rights and obligations of the parties hereto shall be governed, construed and interpreted in accordance with the laws of India, without giving effect to principles of conflicts of law.");

                AddClause(column, "14. Jurisdiction", "The Parties hereby agree that specific Courts in Hyderabad, Telangana, India shall have exclusive jurisdiction over any dispute regarding this Agreement.");

                AddClause(column, "15. Dispute Resolution", "Any dispute arising out of or in connection with this Agreement, including any question regarding its existence, validity or termination, shall be referred to and finally resolved by arbitration in accordance with the Arbitration and Conciliation Act, 1996. The seat of the arbitration shall be Hyderabad.");

                AddClause(column, "16. Entire Agreement", "This Agreement constitutes the entire agreement between the parties with respect to the subject matter hereof and supersedes all prior agreements and understandings, both written and oral, between the parties with respect to the subject matter hereof.");

                AddClause(column, "17. Amendments", "Any amendment or modification of this Agreement or any waiver of rights hereunder must be in writing and signed by authorized representatives of both parties.");

                AddClause(column, "18. Severability", "If any provision of this Agreement is held to be illegal, invalid or unenforceable, that provision shall be severed and the remainder of this Agreement shall remain in full force and effect.");

                AddClause(column, "19. Waiver", "The failure of either party to enforce its rights under this Agreement at any time for any period shall not be construed as a waiver of such rights.");

                AddClause(column, "20. Notices", "All notices or other communications required or permitted hereunder shall be in writing and shall be deemed to have been duly given when delivered personally or sent by registered mail, return receipt requested.");

                AddClause(column, "21. Assignment", "Neither party may assign its rights or obligations under this Agreement without the prior written consent of the other party.");

                AddClause(column, "22. Binding Effect", "This Agreement shall be binding upon and inure to the benefit of the parties hereto and their respective successors and permitted assigns.");

                AddClause(column, "23. No Partnership", "Nothing in this Agreement shall be deemed to constitute a partnership or joint venture between the parties or constitute any party the agent of the other party.");

                AddClause(column, "24. Non-Solicitation", "During the term of this Agreement and for a period of twelve (12) months thereafter, Recipient shall not, directly or indirectly, solicit for employment or employ any employee of Discloser with whom Recipient has had contact or who became known to Recipient in connection with the Purpose.");

                AddClause(column, "25. Export Control", "The parties acknowledge that confidential information disclosed under this agreement may be subject to export control laws and regulations. The Recipient agrees to comply with all applicable export control laws.");

                AddClause(column, "26. Counterparts", "This Agreement may be executed in counterparts, each of which shall be deemed an original, but all of which together shall constitute one and the same instrument.");

                AddClause(column, "27. Representation", "Each party represents and warrants that it has the legal power and authority to enter into this Agreement and that the person signing this Agreement on behalf of such party is duly authorized to do so.");

                AddClause(column, "28. Survival", "The provisions of this Agreement which by their nature are intended to survive the termination or expiration of this Agreement shall continue as valid and enforceable rights and obligations.");

                AddClause(column, "29. Headings", "The headings in this Agreement are for convenience of reference only and shall not limit or otherwise affect the meaning thereof.");

                AddClause(column, "30. Further Assurances", "Each party agrees to execute and deliver such other documents and do such other acts and things as may be necessary or desirable to carry out the provisions of this Agreement.");

                AddClause(column, "31. Public Announcements", "Neither party shall make any public announcement concerning this Agreement or the Purpose without the prior written consent of the other party.");

                AddClause(column, "32. Execution", "IN WITNESS WHEREOF, the Parties hereto have executed this Agreement as of the date set forth above.");

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

        private static void AddClause(ColumnDescriptor column, string title, string content)
        {
            column.Item().PaddingTop(10).Text(text =>
            {
                text.Span(title + ": ").Bold();
                text.Span(content);
            });
        }
    }
}

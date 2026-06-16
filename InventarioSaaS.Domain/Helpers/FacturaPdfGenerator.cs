using InventarioSaaS.Domain.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public static class FacturaPdfGenerator
{
    public static byte[] Generar(LeerVentaDtoUnidad venta)
    {
        QuestPDF.Settings.License =
            LicenseType.Community;

        var primaryColor = "#F4B400";
        var darkColor = "#2D2D2D";

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Margin(40);

                page.DefaultTextStyle(x =>
                    x.FontSize(11));

                // ===== HEADER =====

                page.Header().Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item()
                                .Text("Zentra Business")
                                .FontSize(24)
                                .Bold();

                            left.Item()
                                .Text("Sistema de Facturación")
                                .FontColor(Colors.Grey.Medium);
                        });

                        row.ConstantItem(220)
                            .AlignRight()
                            .Text("INVOICE")
                            .FontSize(34)
                            .Bold()
                            .FontColor(darkColor);
                    });

                    col.Item()
                        .PaddingVertical(15)
                        .LineHorizontal(3)
                        .LineColor(primaryColor);
                });

                // ===== CONTENT =====

                page.Content().PaddingVertical(20).Column(col =>
                {
                    col.Spacing(20);

                    // CLIENTE + FACTURA INFO

                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item()
                                .Text("Factura para:")
                                .Bold();

                            left.Item()
                                .Text(
                                    venta.Cliente?.Nombre
                                    ?? "Consumidor Final");

                            left.Item()
                                .Text(
                                    venta.Cliente?.NumeroTelefono
                                    ?? "");
                        });

                        row.ConstantItem(200)
                            .Column(right =>
                            {
                                right.Item().Row(r =>
                                {
                                    r.RelativeItem()
                                        .Text("Factura #")
                                        .Bold();

                                    r.ConstantItem(80)
                                        .AlignRight()
                                        .Text(venta.Id.ToString());
                                });

                                right.Item().Row(r =>
                                {
                                    r.RelativeItem()
                                        .Text("Fecha")
                                        .Bold();

                                    r.ConstantItem(80)
                                        .AlignRight()
                                        .Text(
                                            venta.Fecha
                                            .ToString("dd/MM/yyyy"));
                                });

                                right.Item().Row(r =>
                                {
                                    r.RelativeItem()
                                        .Text("Pago")
                                        .Bold();

                                    r.ConstantItem(80)
                                        .AlignRight()
                                        .Text(
                                            venta.TipoPago
                                            .ToString());
                                });
                            });
                    });

                    // ===== TABLA =====

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        // HEADER

                        table.Header(header =>
                        {
                            static IContainer Style(IContainer c)
                            {
                                return c
                                    .Background("#2D2D2D")
                                    .PaddingVertical(10)
                                    .PaddingHorizontal(8);
                            }

                            header.Cell()
                                .Element(Style)
                                .Text("#")
                                .FontColor(Colors.White)
                                .Bold();

                            header.Cell()
                                .Element(Style)
                                .Text("Producto")
                                .FontColor(Colors.White)
                                .Bold();

                            header.Cell()
                                .Element(Style)
                                .AlignCenter()
                                .Text("Precio")
                                .FontColor(Colors.White)
                                .Bold();

                            header.Cell()
                                .Element(Style)
                                .AlignCenter()
                                .Text("Cant.")
                                .FontColor(Colors.White)
                                .Bold();

                            header.Cell()
                                .Element(Style)
                                .AlignRight()
                                .Text("Total")
                                .FontColor(Colors.White)
                                .Bold();
                        });

                        int index = 1;

                        foreach (var item in venta.detalle)
                        {
                            var bg =
                                index % 2 == 0
                                ? "#F7F7F7"
                                : "#FFFFFF";

                            table.Cell()
                                .Background(bg)
                                .Padding(10)
                                .Text(index.ToString());

                            table.Cell()
                                .Background(bg)
                                .Padding(10)
                                .Text(item.Nombre);

                            table.Cell()
                                .Background(bg)
                                .Padding(10)
                                .AlignCenter()
                                .Text(item.PrecioUnitario.ToString("C"));

                            table.Cell()
                                .Background(bg)
                                .Padding(10)
                                .AlignCenter()
                                .Text(item.Cantidad.ToString());

                            table.Cell()
                                .Background(bg)
                                .Padding(10)
                                .AlignRight()
                                .Text(item.SubTotal.ToString("C"));

                            index++;
                        }
                    });

                    // ===== TOTAL =====

                    col.Item().AlignRight().Width(220).Column(total =>
                    {
                        total.Item().Row(r =>
                        {
                            r.RelativeItem()
                                .Text("SubTotal");

                            r.ConstantItem(100)
                                .AlignRight()
                                .Text(venta.Total.ToString("C"));
                        });

                        total.Item().Row(r =>
                        {
                            r.RelativeItem()
                                .Text("ITBIS");

                            r.ConstantItem(100)
                                .AlignRight()
                                .Text("$0.00");
                        });

                        total.Item()
                            .Background(primaryColor)
                            .Padding(12)
                            .Row(r =>
                            {
                                r.RelativeItem()
                                    .Text("TOTAL:")
                                    .Bold();

                                r.ConstantItem(100)
                                    .AlignRight()
                                    .Text(venta.Total.ToString("C"))
                                    .Bold();
                            });
                    });
                });

                // ===== FOOTER =====

                page.Footer().Column(col =>
                {
                    col.Item()
                        .PaddingTop(10)
                        .LineHorizontal(2)
                        .LineColor(primaryColor);

                    col.Item()
                        .PaddingTop(10)
                        .AlignCenter()
                        .Text("Gracias por su compra")
                        .FontColor(Colors.Grey.Medium);
                });
            });
        }).GeneratePdf();
    }
}
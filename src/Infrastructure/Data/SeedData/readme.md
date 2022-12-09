

## Json Generator [link]("https://json-generator.com/")

Json format

    [
      '{{repeat(5, 7)}}',
      {
        Name: '{{firstName()}}',
        Price: '{{floating(1000, 4000, 2, "0.00")}}',
        PictureUrl: 'images/products/sb-ang{{index()+1}}.png',
        ProductTypeId: '{{integer(1, 4)}}',
        ProductBrandId: '{{integer(1, 5)}}',
        Description: '{{lorem(1, "paragraphs")}}'
      }
    ]
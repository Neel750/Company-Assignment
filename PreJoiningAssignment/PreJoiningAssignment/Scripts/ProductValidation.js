$(function () {
    //Checks Element not contain any whitespace
    $.validator.addMethod("nowhitespace", function (value, element) {
        return this.optional(element)
            || /^\S+$/i.test(value);
    }, "No white space please");
    //Checks Element not contain any alphabet
    $.validator.addMethod("numbersonly", function (value, element) {
        return this.optional(element)
            || /^[0-9]+$/i.test(value);
    }, "Numbers only please");

    //Checks Element contains only letters
    $.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element)
            || /^[a-z]+$/i.test(value);
    }, "Letters only please");

    //Validate Form method
    var validator = $("#productData").validate({
        rules: {
            ProductName: {
                required: true,
                lettersonly: true
            },
            Category: {
                required: true,
                nowhitespace: true,
                lettersonly: true
            },
            SmallDescription: {
                required: true
            },
            smallFile: {
                required: true
            },
            LongDescription: {
                required: true
            },
            largeFile: {
                required: true
            },
            Price: {
                required: true,
                numbersonly: true
            },
            Quantity: {
                required: true,
                numbersonly: true
            }
        },
        messages: {
            ProductName: {
                required: "Product Name Is Required!",
                lettersonly: "Product Name Is In Letters Only!"
            },
            Category: {
                required: "Category Is Required!"
            },
            SmallDescription: {
                required: "Small Description Is Required!"
            },
            smallFile: {
                required: "File Is Required!"
            },
            LongDescription: {
                required: "Long Description Is Required!"
            },
            largeFile: {
                required: "File Is Required!"
            },
            Price: {
                required: "Price Is Required!",
                numbersonly: "Price In Number Only!"
            },
            Quantity: {
                required: "Quantity Is Required!",
                numbersonly: "Quantity Is Number Only!",
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});
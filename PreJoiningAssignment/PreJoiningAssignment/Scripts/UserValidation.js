$(function () {
    var validator = $("#userData").validate({
        rules: {
            Name: {
                required: true,
                lettersonly: true
            },
            Address: {
                required: true
            },
            Contact: {
                required: true
            },
            Email: {
                required: true,
                email : true
            },
            Password: {
                required: true,
                minlength: 6,
                strongPassword: true
            },
            confirm_password: {
                required: true,
                equalTo: "#password"
            }
        },
        messages: {
            Name: {
                required: "Name Is Required!",
                lettersonly: "Name Is In Letters Only!"
            },
            Address: {
                required: "Address Is Required!"
            },
            Contact: {
                required: "Contact Is Required!"
            },
            Email: {
                required: "Email Is Required!",
                email: "Please Enter Valid Email."
            },
            Password: {
                required: "Password Is Required.",
                minlength: "Password Must Be 6 Character Long.",
                strongPassword: "Password Must Be Strong."
            },
            confirm_password: {
                required: "Confirm Password Is Required. CS",
                equalTo: "Confirm-Password And Password Must Be Same. CS"
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
})
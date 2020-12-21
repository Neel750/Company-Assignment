//Configuration done in view.
//default validation: required, email, minlength
//custom validation: strongPassword, nowhitespace, lettersonly
//methods: validate, addMethod
$(function () {
    //Validates Password is Strong or Not.
    $.validator.addMethod('strongPassword', function (value, element) {
        return this.optional(element)
            || /\d/.test(value)
            && /[a-z]/i.test(value);
    }, 'Password must contain at least one number and one character\'.')

    //Checks Element not contain any whitespace
    $.validator.addMethod("nowhitespace", function (value, element) {
        return this.optional(element)
            || /^\S+$/i.test(value);
    }, "No white space please");

    //Checks Element contains only letters
    $.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element)
            || /^[a-z]+$/i.test(value);
    }, "Letters only please");

    //Validate Form method
    var validator = $("#registerForm").validate({
        rules: {
            fname: {
                required: true,
                nowhitespace: true,
                lettersonly: true
            },
            mname: {
                required: true,
                nowhitespace: true,
                lettersonly: true
            },
            lname: {
                required: true,
                nowhitespace: true,
                lettersonly: true
            },
            uname: {
                required: true,
                nowhitespace: true
            },
            age: {
                required: true
            },
            bday: {
                required: true
            },
            address: {
                required: true
            },
            contact: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            pwd: {
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
            fname: {
                required: "First Name Is Required. CS"
            },
            mname: {
                required: "Middle Name Is Required. CS"
            },
            lname: {
                required: "Last Name Is Required. CS"
            },
            uname: {
                required: "User Name Is Required. CS"
            },
            age: {
                required: "Age Is Required. CS"
            },
            bday: {
                required: "Birth Day Is Required. CS"
            },
            address: {
                required: "Address Is Required. CS"
            },
            contact: {
                required: "Contact No. Is Required. CS"
            },
            email: {
                required: "E-Mail Is Required. CS",
                email: "Please Enter Valid E-Mail. CS"
            },
            pwd: {
                required: "Password Is Required. CS",
                minlength: "Password Must Be 6 Character Long. CS"
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
});
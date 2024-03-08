// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // customer.js content
    $("form").submit(function (event) {
        event.preventDefault();

        $.get($(this).attr("action"), $(this).serialize(), function (data) {
            $("table").replaceWith($(data).find("table"));
        });
    });

    // Survey.js content
    $(".view-more").click(function() {
        var target = $(this).data("target");
        var toggleText = $(this).data("toggle-text");
        
        if ($(target).hasClass("truncate")) {
            $(target).removeClass("truncate");
            $(this).text(toggleText);
            $(this).data("toggle-text", "View More");
        } else {
            $(target).addClass("truncate");
            $(this).text(toggleText);
            $(this).data("toggle-text", "View Less");
        }
    });

    function functionOne() {
        console.log("Function One called");
    }
    
    function functionTwo() {
        console.log("Function Two called");
        // Implement your function logic here
    }
    
    function functionThree() {
        console.log("Function Three called");
        // Implement your function logic here
    }
    
   
       /* ================================================================
       js script for create customer
       ================================================================ */

       $(document).ready(function () {
        // Handle form submission with AJAX
        $("form").submit(function (event) {
            event.preventDefault();
            $.get($(this).attr("action"), $(this).serialize(), function (data) {
                $("table").replaceWith($(data).find("table"));
            });
        });
    
        // Toggle view for truncated content
        $(".view-more").click(function() {
            var target = $(this).data("target");
            var toggleText = $(this).data("toggle-text");
            $(target).toggleClass("truncate");
            $(this).text($(target).hasClass("truncate") ? "View More" : "View Less");
            $(this).data("toggle-text", toggleText);
        });
    
        // Toggle visibility of Customer Type radio buttons
        $("input[name='TypeOption']").click(function() {
            var selectedType = $(this).attr('id');
            // Assuming the id values are 'individual' and 'company'
            var otherType = selectedType === 'individual' ? 'company' : 'individual';
            $('#' + otherType).closest('.custom-control').toggle();
        });
    
        // Dynamically select Payment Terms based on the chosen Type
        $('input[name="TypeOption"]').change(function () {
            var type = $(this).val();
            var termsMapping = {
                "Individual": "customer",
                "Company": "business Terms",
                // Add other mappings as necessary
            };
            var termsOption = termsMapping[type] || "";
            $(`input[name="PaymentTermsOption"][value="${termsOption}"]`).prop("checked", true);
        });
    });
    

    //    $(document).ready(function () {
    //     // Handle form submission with AJAX
    //     $("form").submit(function (event) {
    //         event.preventDefault();
    //         $.get($(this).attr("action"), $(this).serialize(), function (data) {
    //             $("table").replaceWith($(data).find("table"));
    //         });
    //     });
    
    //     // Toggle view for truncated content
    //     $(".view-more").click(function() {
    //         var target = $(this).data("target");
    //         var toggleText = $(this).data("toggle-text");
    //         $(target).toggleClass("truncate");
    //         $(this).text($(target).hasClass("truncate") ? "View More" : "View Less");
    //         $(this).data("toggle-text", toggleText);
    //     });
    
    //     // Toggle visibility of Customer Type radio buttons
    //     $("input[name='TypeOption']").click(function() {
    //         var selectedType = $(this).attr('id');
    //         if(selectedType === 'individual'){
    //             $('#company').closest('.custom-control').toggle();
    //         } else if(selectedType === 'company'){
    //             $('#individual').closest('.custom-control').toggle();
    //         }
    //     });
    
    //     // Dynamically select Payment Terms based on the chosen Type
    //     $('input[name="TypeOption"]').change(function () {
    //         var type = $(this).val();
    //         var termsMapping = {
    //             "Individual": "customer",
    //             "Company": "business Terms",
    //             "Contracted": "Contract terms",
    //             "Supplier": "accountspayable"
    //         };
    //         var termsOption = termsMapping[type] || "";
    //         $(`input[name="PaymentTermsOption"][value="${termsOption}"]`).prop("checked", true);
    //     });
    // });
    
     
// $(document).ready(function(){
//     // When any radio button is clicked
//     $("input[name='TypeOption']").click(function(){
//         // Get the id of the clicked radio button
//         var selectedType = $(this).attr('id');

//         // Toggle the visibility of the other radio button
//         if(selectedType === 'individual'){
//             $('#company').closest('.custom-control').toggle();
//         } else if(selectedType === 'company'){
//             $('#individual').closest('.custom-control').toggle();
//         }
//     });
//     });


//  // The JavaScript for dynamically selecting Payment Terms based on the chosen Type.
//  $(function () {
//     $('input[name="TypeOption"]').change(function () {
//         let type = $(this).val();
//         let termsOption = "";

//         switch (type) {
//             case "Individual":
//                 termsOption = "customer";
//                 break;
//             case "Company":
//                 termsOption = "business Terms";
//                 break;
//             case "Contracted":
//                 termsOption = "Contract terms";
//                 break;
//             case "Supplier":
//                 termsOption = "accountspayable";
//                 break;
//         }

//         $(`input[name="PaymentTermsOption"][value="${termsOption}"]`).prop("checked", true);
//     });
//     });

});


//js  MIN LINK -https://www.toptal.com/developers/javascript-minifier- path link= <script src="~/js/site.min.js" defer></script>



       /* ================================================================
       js script for create customer
       ================================================================ */

    //    <!-- Type - radio button group for CustomerType {Individual, Company, Contracted }-->
    //     @* <div class="form-group mt-3">
    //     <label class="d-block">Customer Type:</label>

    //     <!-- The radio buttons for Type -->
    //     @foreach (var type in new[] { "Individual", "Company", " Contracted" })
    //     {
    //     <div class="custom-control custom-radio">
    //     <input type="radio" id="@type.ToLower()" name="TypeOption" value="@type" class="custom-control-input"
    //     asp-for="Type">
    //     <label class="custom-control-label" for="@type.ToLower()">@type</label>
    //     </div>
    //     }
    //     <span asp-validation-for="Type" class="text-danger"></span>
    //     </div> *@

// <!-- Payment Terms -->
// <div class="form-group mt-3">
//     <label class="d-block">Payment Terms:</label>

//     @foreach (var term in new[] { "Customer", "Business terms", "Contract terms", "Accounts payable" })
//     {
//         <div class="custom-control custom-radio">
//             <input type="radio" id="@term.Replace(" ", "").ToLower()" name="PaymentTermsOption" value="@term"
//                 class="custom-control-input" asp-for="PaymentTerms">
//             <label class="custom-control-label" for="@term.Replace(" ", "").ToLower()">@term</label>
//         </div>
//     }
//     <span asp-validation-for="PaymentTerms" class="text-danger"></span>
// </div>
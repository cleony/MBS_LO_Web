$(function () {
    $("[id$=txtAccountNo]").autocomplete({

        source: function (request, response) {
            $.ajax({
                url: "dataservice.aspx/GetLoanBySearchId",
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('#')[0],
                            val: item.split('#')[1]
                        }
                    }));
                }
               ,
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }

            });
        },
        select: function (e, i) {
            var personinfo = i.item.label;
            var personnameArr = personinfo.toString().split(':');
            $("[id$=txtAccountName]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});


 

$(function () {
    $("[id$=txtAccountNo2]").autocomplete({

        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetLoanBySearchId",
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('#')[0],
                            val: item.split('#')[1]
                        }
                    }));
                }
               ,
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }

            });
        },
        select: function (e, i) {
            var personinfo = i.item.label;
            var personnameArr = personinfo.toString().split(':');
            $("[id$=txtAccountName2]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});


 

  
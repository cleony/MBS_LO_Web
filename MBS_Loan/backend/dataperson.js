$(function () {
    $("[id$=txtPersonId]").autocomplete({

        source: function (request, response) {
            $.ajax({
                url: "dataservice.aspx/GetPersonById",
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
          $("[id$=txtPersonName]").val(personnameArr[1]);
         this.value = i.item.val;
         return false;
        },
        minLength: 1
    });
});
 
 


// personid2
$(function () {
    $("[id$=txtPersonId2]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonById",
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
            $("[id$=txtPersonName2]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});

 
$(function () {
    $("[id$=txtPersonId3]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonById",
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
            $("[id$=txtPersonName3]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});

 

$(function () {
    $("[id$=txtPersonId4]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonById",
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
            $("[id$=txtPersonName4]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});
 
$(function () {
    $("[id$=txtPersonId5]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonById",
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
            $("[id$=txtPersonName5]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});

 

$(function () {
    $("[id$=txtPersonId6]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonById",
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
            $("[id$=txtPersonName6]").val(personnameArr[1]);
            this.value = i.item.val;
            return false;
        },
        minLength: 1
    });
});

 
// guarater1
$(function () {
    $("[id$=txtGTIdCard1]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByIdCard",
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
            $("[id$=txtGTName1]").val(i.item.val);
        },
        minLength: 1
    });
});

$(function () {
    $("[id$=txtGTName1]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByNameIdCard",
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
            $("[id$=txtGTIdCard1]").val(i.item.val);
        },
        minLength: 1
    });
});

// guarater2
$(function () {
    $("[id$=txtGTIdCard2]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByIdCard",
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
            $("[id$=txtGTName2]").val(i.item.val);
        },
        minLength: 1
    });
});

$(function () {
    $("[id$=txtGTName2]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByNameIdCard",
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
            $("[id$=txtGTIdCard2]").val(i.item.val);
        },
        minLength: 1
    });
});

// guarater3
$(function () {
    $("[id$=txtGTIdCard3]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByIdCard",
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
            $("[id$=txtGTName3]").val(i.item.val);
        },
        minLength: 1
    });
});

$(function () {
    $("[id$=txtGTName3]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByNameIdCard",
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
            $("[id$=txtGTIdCard3]").val(i.item.val);
        },
        minLength: 1
    });
});

// guarater4
$(function () {
    $("[id$=txtGTIdCard4]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByIdCard",
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
            $("[id$=txtGTName4]").val(i.item.val);
        },
        minLength: 1
    });
});

$(function () {
    $("[id$=txtGTName4]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByNameIdCard",
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
            $("[id$=txtGTIdCard4]").val(i.item.val);
        },
        minLength: 1
    });
});

// guarater5
$(function () {
    $("[id$=txtGTIdCard5]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByIdCard",
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
            $("[id$=txtGTName5]").val(i.item.val);
        },
        minLength: 1
    });
});

$(function () {
    $("[id$=txtGTName5]").autocomplete({
        source: function (request, response) {
            $.ajax({
                 url: "dataservice.aspx/GetPersonByNameIdCard",
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
            $("[id$=txtGTIdCard5]").val(i.item.val);
        },
        minLength: 1
    });
});


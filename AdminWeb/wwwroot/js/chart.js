
class ChartController {
    constructor() {
        /*var xValuestemp = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        var yValuestemp = [55, 49, 44, 24, 25, 49, 44, 24, 25, 49, 44, 24];*/
        var xValues = [];
        var yValues = [];
        var xValuesMonth = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        var yValuesMonth = [55, 49, 44, 24, 25, 49, 44, 24, 25, 49, 44, 24];
        var barColors = ["red", "green", "blue", "orange", "brown", "pink", "Yellow ", "Gray ", "Purple ", "violet", "Black ", "Orchid "];
        $(window).on('load', function () {
            $.ajax({
                type: "GET",
                url: "/HoaDons/GetIndexAsJson",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    console.log(response.data);
                    var temp = 0
                    var tong = 0;
                    var tongtatca2022 = 0
                    var leng = response.data.length
                    var y = 0;
                    var nam = 0;
                    console.log(leng)
                    for (var i = 0; i < response.data.length; i++) {

                        var hoadon = response.data[i]
                        let d = new Date(hoadon.ngayGio)
                        nam = (d.getFullYear())
                        temp = (d.getMonth() + 1)
                        break
                    }
                    for (var i = 0; i < response.data.length; i++) {
                        var hoadon = response.data[i]
                        let d = new Date(hoadon.ngayGio)
                        /*console.log(d.getFullYear())
                        console.log(d.getMonth()+1)
                        console.log(d.getDate())*/
                        y = y + 1;
                        if (xValues.includes(d.getMonth() + 1) == false) {
                            xValues.push(d.getMonth() + 1)
                        }


                        tongtatca2022 = tongtatca2022 + hoadon.gia
                        if (temp != (d.getMonth() + 1)) {
                            yValues.push(tong)
                            temp = d.getMonth() + 1
                            tong = hoadon.gia;

                        }
                        else {
                            tong = tong + hoadon.gia
                        }
                        if (temp == (d.getMonth() + 1) && y == leng) {
                            yValues.push(tong)
                        }
                        /*if (temp == (d.getMonth() + 1))*/
                    }
                    var row = `<canvas id="myChart" style=$"width: 100 %"></canvas>`
                    $('.js__canvas').html(row);
                    console.log(xValues)
                    console.log(yValues)
                    console.log(tongtatca2022)
                    new Chart("myChart", {
                        type: "bar",
                        options: {
                            legend: { display: false },
                            title: {
                                display: true,
                                text: "Các tháng bán được sản phẩm trong năm " + nam.toString()
                            }
                            
                        },
                        data: {
                            labels: xValues,
                            datasets: [{
                                backgroundColor: barColors,
                                data: yValues
                            }]
                        },
                        
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {

            });
            var xValues = [];
            var yValues = [];


        });
        $('.change-year').off('click').on('click', function () {
            var yearneed = $('.take-year').val();
            var monthneed = $('.take-month').val();
            if (monthneed == null || monthneed=="") {
                $.ajax({
                    type: "GET",
                    url: "/HoaDons/GetIndexAsJson?year=" + yearneed,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        console.log(response.data);
                        var temp = 0
                        var tong = 0;
                        var tongtatca2022 = 0
                        var leng = response.data.length
                        var y = 0;
                        var nam = 0;
                        console.log(leng)
                        for (var i = 0; i < response.data.length; i++) {

                            var hoadon = response.data[i]
                            let d = new Date(hoadon.ngayGio)
                            nam = (d.getFullYear())
                            temp = (d.getMonth() + 1)
                            break
                        }
                        for (var i = 0; i < response.data.length; i++) {
                            var hoadon = response.data[i]
                            let d = new Date(hoadon.ngayGio)
                            /*console.log(d.getFullYear())
                            console.log(d.getMonth()+1)
                            console.log(d.getDate())*/
                            y = y + 1;
                            if (xValues.includes(d.getMonth() + 1) == false) {
                                xValues.push(d.getMonth() + 1)
                            }


                            tongtatca2022 = tongtatca2022 + hoadon.gia
                            if (temp != (d.getMonth() + 1)) {
                                yValues.push(tong)
                                temp = d.getMonth() + 1
                                tong = hoadon.gia;

                            }
                            else {
                                tong = tong + hoadon.gia
                            }
                            if (temp == (d.getMonth() + 1) && y == leng) {
                                yValues.push(tong)
                            }
                            /*if (temp == (d.getMonth() + 1))*/
                        }
                        console.log(xValues)
                        console.log(yValues)
                        console.log(tongtatca2022)
                        var row = `<canvas id="myChart" style=$"width: 100 %"></canvas>`
                        $('.js__canvas').html(row);
                        new Chart("myChart", {
                            type: "bar",
                            data: {
                                labels: xValues,
                                datasets: [{
                                    backgroundColor: barColors,
                                    data: yValues
                                }]
                            },
                            options: {
                                legend: { display: false },
                                title: {
                                    display: true,
                                    text: "Các tháng bán được sản phẩm trong năm " + nam.toString()
                                }
                            }
                        });
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {

                });
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/HoaDons/GetIndexAsJson?year=" + yearneed + "&month=" + monthneed,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.data.length != 0) {
                            console.log(response.data);
                            var temp = 0
                            var tong = 0;
                            var tongtatca2022 = 0
                            var leng = response.data.length
                            var y = 0;
                            var nam = 0;
                            var thang = 0;
                            console.log(leng)
                            for (var i = 0; i < response.data.length; i++) {

                                var hoadon = response.data[i]
                                let d = new Date(hoadon.ngayGio)
                                nam = (d.getFullYear())
                                thang = (d.getMonth() + 1)
                                temp = (d.getDate())
                                break
                            }
                            for (var i = 0; i < response.data.length; i++) {
                                var hoadon = response.data[i]
                                let d = new Date(hoadon.ngayGio)
                                /*console.log(d.getFullYear())
                                console.log(d.getMonth()+1)
                                console.log(d.getDate())*/
                                y = y + 1;
                                if (xValues.includes(d.getDate()) == false) {
                                    xValues.push(d.getDate())
                                }


                                tongtatca2022 = tongtatca2022 + hoadon.gia
                                if (temp != (d.getDate())) {
                                    yValues.push(tong)
                                    temp = d.getDate()
                                    tong = hoadon.gia;

                                }
                                else {
                                    tong = tong + hoadon.gia
                                }
                                if (temp == (d.getDate()) && y == leng) {
                                    yValues.push(tong)
                                }
                                /*if (temp == (d.getMonth() + 1))*/
                            }
                            console.log(xValues)
                            console.log(yValues)
                            console.log(tongtatca2022)
                            var row = `<canvas id="myChart" style=$"width: 100 %"></canvas>`
                            $('.js__canvas').html(row);
                            new Chart("myChart", {
                                type: "bar",
                                data: {
                                    labels: xValues,
                                    datasets: [{
                                        backgroundColor: barColors,
                                        data: yValues
                                    }]
                                },
                                options: {
                                    legend: { display: false },
                                    title: {
                                        display: true,
                                        text: "Các ngày bán được sản phẩm trong tháng " + thang.toString() + " năm " + nam.toString()
                                    }
                                }
                            });
                        }
                        else {
                            console.log(response.data)
                            var log = `<p>Tháng vừa rồi bạn chưa kiếm được gì !</p>`
                            $('.log-to-you').html(log);
                        }
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {

                });
            }
            var xValues = [];
            var yValues = [];


        });
        
    }

}
new ChartController();

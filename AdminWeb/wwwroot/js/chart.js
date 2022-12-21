
class ChartController {
    constructor() {
        var xValuestemp = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        var yValuestemp = [55, 49, 44, 24, 25, 49, 44, 24, 25, 49, 44, 24];
        var xValues = [];
        var yValues = [];
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
                    console.log(leng)
                    for (var i = 0; i < response.data.length; i++) {

                        var hoadon = response.data[i]
                        let d = new Date(hoadon.ngayGio)
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
                                text: "Các tháng bán được sản phẩm trong năm"
                            }
                        }
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {

            });



        });
    }

}
new ChartController();

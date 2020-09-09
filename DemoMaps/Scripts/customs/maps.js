$(function () {
    getData();
});
/**
 * Create the chart when all data is loaded
 * @returns {undefined}
 */
function createChart(seriesOptions) {

    Highcharts.stockChart('container', {

        rangeSelector: {
            selected: 4
        },

        yAxis: {
            labels: {
                formatter: function () {
                    return this.value;
                }
            },
            plotLines: [{
                value: 0,
                width: 2,
                color: 'silver'
            }]
        },

        plotOptions: {
            series: {
               
                showInNavigator: true
            }
        },

        tooltip: {
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>',
            valueDecimals: 2,
            split: true
        },
        title: {
            text: "Biểu đồ giá vàng SJC 1 năm - <small>từ ngày 09/09/2019 đến ngày 09/09/2020</small> (đơn vị triệu đồng / lượng)"
        },
        subtitle: {
            text: "<br><strong style='color:#008000'>–––</strong> VN <strong style='color:#ff0000; padding-left:20px'>–––</strong> TG",
            useHTML: true
        },
        colors: ["green", "red", "#90ed7d", "#f7a35c", "#8085e9", "#f15c80", "#e4d354", "#2b908f", "#f45b5b", "#91e8e1"],
        series: seriesOptions
    });
}

function getData() {
    $('.loading').show();
    $.ajax({
        url: "/Home/GetData",
        success: function (response) {
            var seriesOptions = [];
            var obj = JSON.parse(response.VN);
            seriesOptions[0] = {
                name: "VN",
                data: obj
            };
            obj = JSON.parse(response.TG);
            seriesOptions[1] = {
                name: "TG",
                data: obj
            };
            createChart(seriesOptions);
            $('.loading').hide();
        },
        error: function (e) {
            alert(e);
        }
    });
  
}

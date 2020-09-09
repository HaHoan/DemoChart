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
                     return (this.value > 0 ? ' + ' : '') + this.value + '%';
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
                compare: 'percent',
                showInNavigator: true
            }
        },

        tooltip: {
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.change}%)<br/>',
            valueDecimals: 2,
            split: true
        },

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

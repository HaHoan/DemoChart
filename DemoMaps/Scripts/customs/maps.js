var seriesOptions = []
success();
/**
 * Create the chart when all data is loaded
 * @returns {undefined}
 */
function createChart() {

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

function success() {
    var maps_vn = $('#maps_vn').val();
    var maps_tg = $('#maps_tg').val();
    var obj = JSON.parse(maps_vn);
    seriesOptions[0] = {
        name: "VN",
        data: obj
    };
    var obj = JSON.parse(maps_tg);
    seriesOptions[1] = {
        name: "TG",
        data: obj
    };
    createChart();
}

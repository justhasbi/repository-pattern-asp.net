

let pieOptions = {
    chart: {
        type: 'pie',
        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
            }
        }
    },
    dataLabels: {
        enabled: true
    },
    series: [],
    labels: [],
    title: {
        text: 'User Gender'
    },
    noData: {
        text: 'Loading data...'
    },
    legend: {
        position: 'bottom',
        horizontalAlign: 'left'
    }
}

let donutOptions = {
    chart: {
        type: 'donut',
        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
            }
        }
    },
    dataLabels: {
        enabled: true
    },
    series: [],
    labels: [],
    title: {
        text: 'User Education'
    },
    noData: {
        text: 'Loading data...'
    },
    legend: {
        position: 'bottom',
        horizontalAlign: 'left'
    }
}

let barOptions = {
    chart: {
        type: 'bar',
        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
            }
        }
    },
    dataLabels: {
        enabled: true
    },
    series: [{
        data: []
    }],
    labels: [],
    title: {
        text: 'Average GPA User'
    },
    noData: {
        text: 'Loading data...'
    },

}
let barOptionsDegree = {
    chart: {
        type: 'bar',
        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
            }
        }
    },
    dataLabels: {
        enabled: true
    },
    series: [{
        data: []
    }],
    labels: [],
    title: {
        text: 'Number of Users Per Degree'
    },
    noData: {
        text: 'Loading data...'
    },

}

let pieChart = new ApexCharts(document.querySelector("#pie-chart"), pieOptions);
let donutChart = new ApexCharts(document.querySelector("#donut-chart"), donutOptions);
let barChart = new ApexCharts(document.querySelector("#bar-chart"), barOptions);
let barChartDegree = new ApexCharts(document.querySelector("#bar-chart-degree-amount"), barOptionsDegree);

pieChart.render();
donutChart.render();
barChart.render();
barChartDegree.render();

let url = 'https://localhost:44300/api/persons/getperson'

$.getJSON(url, (res) => {
    console.log(res)

    // pie chart data
    const male = res.result.filter(item => item.gender === "Male").length;
    const female = res.result.filter(item => item.gender === "Female").length;
    
    pieChart.updateOptions({
        series: [male, female],
        labels: ['male', 'female']
    })

    // donut chart
    const univName = []
    const university = res.result.filter((v, i, a) => a.findIndex(t => (t.universityName === v.universityName)) === i)
    university.map(item => {
        univName.push(item.universityName)
    })

    const numUniv = []
    univName.forEach(i => {
        numUniv.push(res.result.filter(item => item.universityName === i).length)
    })

    donutChart.updateOptions({
        series: numUniv,
        labels: univName
    })

    // bar chart
    const degreeName = []
    const degree = res.result.filter((v, i, a) => a.findIndex(t => (t.degree === v.degree)) === i)
    degree.map(item => {
        degreeName.push(item.degree)
    })

    const gpaAverage = []
    const degreeAmount = []
    degreeName.forEach(i => {
        const result = res.result.filter(item => item.degree === i)
        const dataAmount = res.result.filter(item => item.degree === i).length
        degreeAmount.push(dataAmount)
        gpaAverage.push((result.reduce((total, next) => total + parseFloat(next.gpa), 0) / result.length).toFixed(3))
    })

    barChart.updateOptions({
        series: [
            {
                data: gpaAverage
            }
        ],
        xaxis: {
            categories: degreeName,
        },
    })

    // bar chart degree amount
    barChartDegree.updateOptions({
        series: [
            {
                data: degreeAmount
            }
        ],
        xaxis: {
            categories: degreeName,
        },
    })
})

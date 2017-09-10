export interface ChartModel {
    type: string;
    data: {
        labels: string[],
        datasets: [
            {
                label: string;
                borderColor: string;
                backgroundColor: string;
                data: number[]
            },
            {
                label: string,
                borderColor: string;
                backgroundColor: string;
                data: number[]
            }
        ]
    };
}

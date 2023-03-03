import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts/highstock';
import HC_exporting from 'highcharts/modules/exporting';
import { SharedService } from './shared.service';
HC_exporting(Highcharts);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'angular-market-app';

  public candlestickData: any = [];

  constructor(private ref: ChangeDetectorRef, private service: SharedService) {}

  ngOnInit() {
    this.getCandlestickData();
  }

  private setupChart(data: any[]) {
    Highcharts.stockChart('chart-container', {
      rangeSelector: {
        selected: 1,
        buttons: [{
          type: 'minute',
          count: 1,
          text: '1m'
        }]
      },
      title: {
        text: 'Candlestick Chart'
      },
      yAxis: [{
        title: {
          text: 'Price'
        },
        height: '70%',
        lineWidth: 2,
        resize: {
          enabled: true
        }
      }, {
        title: {
          text: 'Volume'
        },
        top: '70%',
        height: '30%',
        offset: 0,
        lineWidth: 2
      }],
      series: [{
        type: 'candlestick',
        name: 'Candlesticks',
        data: data.map((candlestick: { time: string; sumVolume: number; open: number; high: number; low: number; close: number; }) =>{
          const isUp: boolean = candlestick.close > candlestick.open;
          return [
            new Date(candlestick.time).getTime(),
            candlestick.open,
            candlestick.high,
            candlestick.low,
            candlestick.close,
            isUp ? '#00FF00' : '#FF0000', // green if close > open, red otherwise
          ]}),
        tooltip: {
          valueDecimals: 5
        },
        color: '#FF0000', // default color for down candlesticks
        upColor: '#00FF00', // color for up candlesticks
      },
      {
        type: 'column',
        name: 'Liquidity',
        data: data.map((candlestick: { time: string; sumVolume: number; }) => [
          new Date(candlestick.time).getTime(),
          candlestick.sumVolume
        ]),
        yAxis: 1,
        color: '#6b8f6e'
      }
    ]
    });
  }

  private getCandlestickData() {
    this.service.getCandleStickList().subscribe(data =>{
      this.candlestickData = data;
      if (this.candlestickData.length > 0) {
        this.setupChart(data);
      }
      this.ref.markForCheck();
    });
  }
}

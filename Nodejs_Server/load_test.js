import http from 'k6/http';
import { sleep, group, check } from 'k6';
import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js';

export const options = {
	stages: [
    { duration: '10s', target: 20 }, // simulate ramp-up of traffic from 1 to 60 users over 5 minutes.
    { duration: '20s', target: 30 }, // stay at 60 users for 10 minutes
    //{ duration: '3m', target: 100 }, // ramp-up to 100 users over 3 minutes (peak hour starts)
    //{ duration: '2m', target: 100 }, // stay at 100 users for short amount of time (peak hour)
    //{ duration: '3m', target: 60 }, // ramp-down to 60 users over 3 minutes (peak hour ends)
    //{ duration: '10m', target: 60 }, // continue at 60 for additional 10 minutes
    //{ duration: '5m', target: 0 }, // ramp-down to 0 users
  ],
  thresholds: {
    http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5s
  },
};

export default function usertests() {
	group('users', function() {
		http.get('http://localhost:3000/user?id=testttt');
		//check(response, {
		//	'can get user': (res) => {
		//		let user = JSON.parse(res.body);
		//		return user !== undefined;
		//	}
		//});
	});

	group('qns', function() {
		let response = http.get('http://localhost:3000/questions?lvl=1');
		check(response, {
			'can get qns': (res) => {
				let user = JSON.parse(res.body);
				return user !== undefined;
			}
		});
	});
  //http.get('http://localhost:3000/user?id=testttt');
  sleep(1);
  //http.get('http://localhost:3000/items?itemType=Accessory&itemSource=Shop');
}



export function handleSummary(data) {
	console.log('Preparing the end-of-test summary...');
  
	return {
	  'stdout': textSummary(data, { indent: ' ', enableColors: true }), // Show the text summary to stdout...
	  'test/summary.json': JSON.stringify(data), // and a JSON with all the details...
	};
  }
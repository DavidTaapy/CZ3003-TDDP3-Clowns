import http from 'k6/http';
import { sleep } from 'k6';
import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js';

export const options = {
	scenarios: {
		smoke_1: {
			executor: 'constant-vus',
			vus: 1,
			duration: '1m',
		},
		smoke_2: {
			executor: 'constant-vus',
			vus: 2,
			duration: '1m',
		},
		load_batches: {
			executor: 'per-vu-iterations',
			vus: 50,
			iterations: 10,
			maxDuration: '1m',
		},
		load_stages: {
			executor: 'ramping-vus',
			stages: [
			{ duration: '20s', target: 20 }, 
			{ duration: '10s', target: 30 }, 
			{ duration: '30s', target: 50 }, 
			{ duration: '10s', target: 50 }, 
			{ duration: '30s', target: 30 }, 
			{ duration: '20s', target: 0 }, 
			]
		},
		stress_stages: {
			executor: 'ramping-vus',
			stages: [
				{ duration: '70s', target: 50 }, // normal load
				{ duration: '10s', target: 50 },
				{ duration: '30s', target: 70 }, 
				{ duration: '30s', target: 90 }, // around the maximum load
				{ duration: '10s', target: 90 },
				{ duration: '40s', target: 60 }, // scale down. Recovery stage.
				{ duration: '30s', target: 30 },
				{ duration: '30s', target: 0 }, 
			  ],
		}
	}
};

export default function loadtest() {
	http.get('http://localhost:3000/questions?lvl=1');
    sleep(1);
}

export function handleSummary(data) {
	console.log('Preparing the end-of-test summary...');
	return {
	  'stdout': textSummary(data, { indent: ' ', enableColors: true }), // Show the text summary to stdout...
	  'test/summary.json': JSON.stringify(data), // and a JSON with all the details...
	};
  }
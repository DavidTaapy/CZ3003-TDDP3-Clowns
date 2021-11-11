import http from 'k6/http';
import { sleep } from 'k6';
import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js';

export const options = {
	vus: 20,
	duration: '30s',
  };

export default function usertest() {
  http.get('http://localhost:3000/user?id=testttt');
  sleep(1);
  //http.get('http://localhost:3000/items?itemType=Accessory&itemSource=Shop');
}

export function itemtest() {
	
	sleep(1);
  }


export function handleSummary(data) {
	console.log('Preparing the end-of-test summary...');
  
	return {
	  'stdout': textSummary(data, { indent: ' ', enableColors: true }), // Show the text summary to stdout...
	  'test/summary.json': JSON.stringify(data), // and a JSON with all the details...
	};
  }
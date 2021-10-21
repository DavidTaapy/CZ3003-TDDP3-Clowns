import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

/*describe('Init', function () {
    it('check app status', function (done) {
      chai.request("http://localhost:3000").get('/').end((err, res) => {
        should.not.exist(err);
        res.should.have.status(200);
        done();
      })
    });
     
});*/
describe('User API', () => {
    //Test GET user
    var user_id = "testttt"

    describe('GET /user', () => {
        it("This should get the user with corresponding id", (done) => {
            chai.request(server)
            .get("/user")
            .query({id: "testttt"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body).to.have.property('id');
                expect(res.body).to.have.property('userName');
                expect(res.body).to.have.property('primaryLevel');
                expect(res.body).to.have.property('id').eq(user_id);
            done();
            })
        })
    })
    
})
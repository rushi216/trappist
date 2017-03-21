import { Injectable } from "@angular/core";
import { HttpService } from "../core/http.service";
import { Test } from "./tests.model";
@Injectable()

export class TestService {

    private testsApiUrl = "api/TestDashboard";
    private testApiUrl = "api/tests";
    test: Test = new Test();
    private testApiUrl = "api/tests";

    constructor(private httpService: HttpService) {
    }
    /**
     * get list of tests
     */
    getTests() {
        return this.httpService.get(this.testsApiUrl);
    }
    /**
     * add new test
     * @param url
     * @param test
     */
    addTests(url: string, test: any) {
        return this.httpService.post(url,test);
    }

    getTest(testName:any) {
        return this.httpService.get(this.testApiUrl+"?testName=" +testName);
    }
}

import { Component } from "@angular/core";
import { ActivatedRoute, Router, provideRoutes } from '@angular/router';
import { Http } from "@angular/http";

@Component({
    moduleId: module.id,
    selector: "tests",
    templateUrl: "tests.html"
})

export class TestsComponent {
    //test: Test = new Test;
    //t_list: Test[] = new Array<Test>();
    //constructor(public http: Http, private route: ActivatedRoute) {
    //    this.http = http;
    //    this.http.get("api/tests").subscribe((response) => {
    //        this.t_list = response.json();
    //    });
    //}
    //AddTest() {
    //    this.http.post("api/tests", this.test).subscribe((response) => {
    //        if (response.ok)
    //            this.t_list.push(response.json());
    //        this.test = new Test;         
    //    });
    //}
}

//export class Test {
   
//}

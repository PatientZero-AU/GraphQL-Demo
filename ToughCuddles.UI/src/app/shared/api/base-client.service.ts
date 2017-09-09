import { Response, RequestOptionsArgs } from '@angular/http'; // ignore

import { ToasterService } from 'angular2-toaster';
import { OAuthService } from 'angular-oauth2-oidc';
import { BaseConfiguration } from "./base-configuration.service";
import { Observable } from "rxjs/Observable"; // ignore

export class BaseClient {

    constructor(baseConfiguration: BaseConfiguration) {
    }

    protected transformOptions(options: RequestOptionsArgs) {

        options.withCredentials = true;
        return Promise.resolve(options);
    }
}

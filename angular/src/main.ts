
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {AppModule} from './app/app.module';
import { registerLocaleData } from '@angular/common';
import localeCs from '@angular/common/locales/cs';

platformBrowserDynamic().bootstrapModule(AppModule).catch(error => {console.log(error);});
registerLocaleData(localeCs);

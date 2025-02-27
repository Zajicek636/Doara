import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Sklady',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44354/',
    redirectUri: baseUrl,
    clientId: 'Sklady_App',
    responseType: 'code',
    scope: 'offline_access Sklady',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44354',
      rootNamespace: 'Sklady',
    },
    Sklady: {
      url: 'https://localhost:44371',
      rootNamespace: 'Sklady',
    },
  },
} as Environment;

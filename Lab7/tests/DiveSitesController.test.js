const http = require('http');
const logger = require('../config/logger');
const { getUserTokens } = require('./utils');
const request = require('supertest');

const PORT = 7142;
const BASE_PATH = '/api';

describe('DiveSites API Integration Tests', ()  => {
    let access_token;

    beforeAll(async () => {
        const tokens = await getUserTokens();
        access_token = tokens.access_token;
    });

    afterAll(() => {
        logger.info('Finished DiveSites API Integration Tests');
    });

    test('GET /dive-sites Should return a list of dive sites', async () => {
        logger.info('Testing GET /dive-sites endpoint');

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/v1/dive-sites`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false,
            }),
            timeout: 10000,
        };

        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                try {
                    expect(res.statusCode).toBe(200);
                    const parsedData = JSON.parse(data);
                    expect(Array.isArray(parsedData)).toBe(true);
                    expect(parsedData.length).toBeGreaterThan(0);
                    expect(parsedData[0]).toHaveProperty('DiveSiteId');
                    expect(parsedData[0]).toHaveProperty('DiveSiteCode');
                    expect(parsedData[0]).toHaveProperty('DiveSiteDescription');
                    expect(parsedData[0]).toHaveProperty('DiveSiteName');
                    expect(parsedData[0]).toHaveProperty('OtherDetails');
                    logger.info('GET /dive-sites passed successfully');
                    done();
                } catch (error) {
                    logger.error('Error parsing response data');
                }
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /dive-sites: ${error.message}`);
        });

        req.end();
    });

    test('GET /dive-sites/:id should return a dive site by ID', async () => {
        logger.info('Testing GET /dive-sites/:id endpoint');

        const diveSiteId = 'd27qr42c-58cc-4372-a567-0e02b2c3d479';

        const options = {
            hostname: 'localhost',
            port: PORT,
            path: `${BASE_PATH}/v1/dive-sites/${diveSiteId}`,
            method: 'GET',
            headers: {
                Authorization: `Bearer ${access_token}`,
            },
            agent: new http.Agent({
                rejectUnauthorized: false,
            }),
            timeout: 10000,
        };

        const req = http.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                try {
                    expect(res.statusCode).toBe(200);
                    const parsedData = JSON.parse(data);
                    expect(parsedData).toHaveProperty('DiveSiteId', diveSiteId);
                    expect(parsedData).toHaveProperty('DiveSiteCode');
                    expect(parsedData).toHaveProperty('DiveSiteDescription');
                    expect(parsedData).toHaveProperty('DiveSiteName');
                    expect(parsedData).toHaveProperty('OtherDetails');
                    logger.info(`GET /dive-sites/${diveSiteId} passed successfully`);
                    done();
                } catch (error) {
                    logger.error('Error parsing response data');
                }
            });
        });

        req.on('error', (error) => {
            logger.error(`Error testing GET /dive-sites/:id: ${error.message}`);
        });

        req.end();
    });
});

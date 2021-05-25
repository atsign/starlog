import { AzureFunction, Context, HttpRequest } from "@azure/functions"

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
    const starNames: string[] = [
        "Vega", "Arcturus", "Procyon", "Polaris", "Regulus"
    ];

    context.res.json({
        star: starNames[Math.ceil(Math.random() * starNames.length - 1)]
    });
};

export default httpTrigger;
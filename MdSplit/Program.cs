using MdSplit;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
    .RunCommandLineApplicationAsync<AppCommand>(args);
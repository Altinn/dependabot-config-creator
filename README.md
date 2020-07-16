# Dependabot Config Creator

Tool for generating a [version 2 dependabot.yml file](https://docs.github.com/en/github/administering-a-repository/configuration-options-for-dependency-updates).

## Usage

```shell
dotnet run --help
```

## Limitations

The tool is currently only scanning for `docker`, `npm`, `nuget` and `maven`,
and not all [package-ecosystems](https://docs.github.com/en/github/administering-a-repository/configuration-options-for-dependency-updates#package-ecosystem).

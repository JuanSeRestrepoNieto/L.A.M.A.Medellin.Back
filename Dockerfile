# Dockerfile para levantar una instancia de SQL Server (Linux)
# Uso recomendado: construir la imagen y pasar SA_PASSWORD al ejecutar:
#   docker build -t my-mssql .
#   docker run -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 -v mssql-data:/var/opt/mssql my-mssql
FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer
EXPOSE 1433

# Copiar scripts de inicialización (opcional). Colocar .sql / .sh en ./initdb junto al Dockerfile.
COPY ./initdb /var/opt/mssql/initdb
RUN chmod -R 755 /var/opt/mssql/initdb || true

# Entrypoint que arranca sqlservr y ejecuta scripts de init (si existen).
RUN bash -c 'cat > /entrypoint.sh << "EOF"\n#!/usr/bin/env bash\nset -e\n\n# Requerir SA_PASSWORD en runtime\nif [ -z \"$SA_PASSWORD\" ]; then\n  echo \"ERROR: SA_PASSWORD environment variable is required\" >&2\n  exit 1\nfi\n\n# Iniciar SQL Server en background\n/opt/mssql/bin/sqlservr &\npid=$!\n\n# Esperar a que el servicio acepte conexiones (max ~60s)\nfor i in {1..60}; do\n  if /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P \"$SA_PASSWORD\" -Q \"SELECT 1\" >/dev/null 2>&1; then\n    break\n  fi\n  echo \"Waiting for SQL Server... ($i)\"\n  sleep 1\ndone\n\n# Ejecutar scripts de inicialización si existen\nif [ -d \"/var/opt/mssql/initdb\" ]; then\n  for f in /var/opt/mssql/initdb/*; do\n    case \"$f\" in\n      *.sh)\n        echo \"Running $f\";\n        chmod +x \"$f\";\n        . \"$f\" ;;\n      *.sql)\n        echo \"Running $f\";\n        /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P \"$SA_PASSWORD\" -i \"$f\" ;;\n      *)\n        echo \"Skipping $f\" ;;\n    esac\n  done\nfi\n\nwait $pid\nEOF'\n\nRUN chmod +x /entrypoint.sh\n\nENTRYPOINT [\"/entrypoint.sh\"]\n```
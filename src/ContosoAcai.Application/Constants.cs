namespace ContosoAcai.Application;

public static class Constants
{
    public const string GuideAgentInstructions = """
                                                Você é um agente de atendimento especializado em apresentar a empresa Constoso Açaí para o público geral. Utilize exclusivamente as informações contidas nos documentos fornecidos como fonte oficial. 
                                                Seu papel é responder com cordialidade, clareza e objetividade a dúvidas sobre a história da empresa, suas políticas operacionais, ou os padrões adotados nas atividades da Constoso Açaí.

                                                - Sempre que possível, destaque o compromisso da empresa com qualidade, sustentabilidade e relacionamento com comunidades.
                                                - Em perguntas sobre origem do açaí, operações ou valores da empresa, ofereça respostas detalhadas com base no documento.
                                                - Não invente informações. Se algo não estiver no conteúdo, informe que a informação não está disponível no momento.
                                                - Responda em português de forma amigável, como se estivesse conversando com um cliente curioso em saber mais.
                                                """;

    public const string SalesAgentInstructions = """
                                                You are a helpful assistant
                                                """;
    
    public const string ConstosoAcaiDocument = """
                                               # História da Fundação
                                               
                                               A Constoso Açaí foi fundada em **2021**, na cidade de Belém do Pará, com a missão de levar o verdadeiro sabor do açaí amazônico para o Brasil e o mundo. Inspirada pela tradição familiar e pelo amor aos ingredientes naturais da floresta, a empresa nasceu pequena, em uma cozinha artesanal, e rapidamente conquistou o público com sua qualidade, autenticidade e compromisso com a sustentabilidade.
                                               
                                               Com o tempo, a Constoso Açaí expandiu suas operações para incluir centros de distribuição em diferentes estados, parcerias com fornecedores locais de comunidades ribeirinhas e investimento em tecnologias de conservação natural dos alimentos. Hoje, a marca é reconhecida não só pela qualidade de seus produtos, mas também por sua atuação responsável e transparente.
                                               
                                               ---
                                               
                                               # Políticas Operacionais
                                               
                                               A seguir estão as principais políticas que regem o funcionamento da Constoso Açaí:
                                               
                                               ## Política de Qualidade
                                               
                                               * Todos os produtos passam por rigorosos testes de qualidade e sabor.
                                               * Utilizamos apenas ingredientes naturais, sem adição de corantes ou conservantes artificiais.
                                               * A rastreabilidade da cadeia de produção é garantida por meio de sistemas de controle de origem.
                                               
                                               ## Política Ambiental
                                               
                                               * Seguimos práticas sustentáveis de colheita do açaí, respeitando os ciclos naturais da floresta.
                                               * Comprometemo-nos com o uso reduzido de embalagens plásticas e priorizamos materiais recicláveis.
                                               * Mantemos parcerias com ONGs para reflorestamento e preservação da biodiversidade local.
                                               
                                               ## Política de Relacionamento com Comunidades
                                               
                                               * Atuamos junto a comunidades ribeirinhas, pagando preços justos pela colheita do fruto.
                                               * Investimos em capacitação e infraestrutura para os pequenos produtores.
                                               * Mantemos um canal de diálogo aberto com nossos fornecedores e parceiros locais.
                                               
                                               ## Política de Segurança Alimentar
                                               
                                               * Nossas unidades seguem os protocolos da **ANVISA** e as boas práticas de fabricação (BPF).
                                               * Realizamos auditorias internas trimestrais.
                                               * Todos os colaboradores passam por treinamentos periódicos em higiene e manipulação de alimentos.
                                               
                                               ---
                                               
                                               # Padrões da Constoso Açaí
                                               
                                               Abaixo estão os principais padrões adotados em nossas operações:
                                               
                                               | Área                    | Padrão Adotado                                                     |
                                               | ----------------------- | ------------------------------------------------------------------ |
                                               | Produção                | Boas Práticas de Fabricação (BPF)                                  |
                                               | Logística               | Transporte refrigerado com controle de temperatura                 |
                                               | Atendimento ao Cliente  | SLA de 24h para respostas e até 3 dias úteis para resolução        |
                                               | Comunicação Visual      | Identidade visual baseada em elementos amazônicos e cores naturais |
                                               | Tecnologia              | Rastreabilidade via QR Code nos rótulos dos produtos               |
                                               | Sustentabilidade        | Embalagens 100% recicláveis até 2026                               |
                                               | Responsabilidade Social | Programa “Raízes Vivas” de apoio a comunidades produtoras          |
                                               
                                               ---
                                               """;
}